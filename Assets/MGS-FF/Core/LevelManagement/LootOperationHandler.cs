using System;
using System.Collections.Generic;
using Collectables;
using Collectables.UI;
using Collectables.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LevelManagement
{
    public class LootOperationHandler : IOperationHandler
    {
        private readonly AssetProvider _assetProvider;
        private readonly CollectableResolver _resolver;
        private readonly CollectableFloatingTextCanvas _canvas;

        private readonly Dictionary<int, string> _keysMap = new()
        {
            {CollectablesTypes.PistolAmmo, "PistolAmmo"}
        };
        
        public LootOperationHandler(
            AssetProvider assetProvider,
            CollectableResolver resolver,
            CollectableFloatingTextCanvas canvas)
        {
            _assetProvider = assetProvider;
            _resolver = resolver;
            _canvas = canvas;
        }

        public Type GetOperationType()
        {
            return  typeof(LootSpawnOperation);
        }

        public async UniTask Handle(IOperation operation)
        {
            if (operation is not LootSpawnOperation lootSpawnOperation)
            {
                throw new Exception("operation is not LootSpawnOperation");
            }

            foreach (var lootEntry in lootSpawnOperation.Entries)
            {
                var prefab = await GetPrefab(lootEntry.LootType);

                if (!prefab)
                {
                    throw new Exception("prefab in config is null");
                }

                var loot = UnityEngine.Object.Instantiate(prefab, lootEntry.Position, Quaternion.identity)
                    .GetComponent<CollectableObjectView>();
                loot.Init(_resolver, new PistolAmmoCollectable(1), _canvas);
            }
        }

        private async UniTask<GameObject> GetPrefab(int type)
        {
            if (!_keysMap.TryGetValue(type, out string key))
            {
                throw new Exception($"_keysMap is not contains key for CollectablesTypes.{key} type");
            }

            return await _assetProvider.LoadAssetAsync<GameObject>(key);
        }
    }
}