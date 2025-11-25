using System.Collections.Generic;
using Collectables;
using UnityEngine;

namespace LevelManagement
{
    public class InitializationConfig
    {
        public List<IOperation> Entries;

        public InitializationConfig()
        {
            Entries = new List<IOperation>()
            {
                new LootSpawnOperation(new[]
                {
                    new LootSpawnEntry(CollectablesTypes.PistolAmmo, Vector3.forward * 2),
                    new LootSpawnEntry(CollectablesTypes.PistolAmmo, Vector3.back * 2)
                })
            };
        }
    }
}