using System;
using Collectables;
using Cysharp.Threading.Tasks;

namespace LevelManagement
{
    public class LootOperationHandler : IOperationHandler
    {
        private readonly CollectableService _collectableService;

        public LootOperationHandler(CollectableService collectableService)
        {
            _collectableService = collectableService;
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
                await _collectableService.SpawnCollectable(lootEntry.Config, lootEntry.Position);
            }
        }
    }
}