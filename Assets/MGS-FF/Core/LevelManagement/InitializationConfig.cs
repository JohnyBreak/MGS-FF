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
                    new LootSpawnEntry(new PistolAmmoConfig{Amount = 15}, Vector3.forward * 2),
                    new LootSpawnEntry(new PistolAmmoConfig{Amount = 15}, Vector3.back * 2)
                })
            };
        }

        public static InitializationConfig EmptyConfig()
        {
            return new InitializationConfig() { Entries = new List<IOperation>() };
        }
    }
}