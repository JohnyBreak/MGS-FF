using Collectables;
using UnityEngine;

namespace LevelManagement
{
    public class LootSpawnOperation : IOperation
    {
        public readonly LootSpawnEntry[] Entries;

        public LootSpawnOperation(LootSpawnEntry[] entries)
        {
            Entries = entries;
        }
    }

    public class LootSpawnEntry
    {
        public readonly CollectableConfig Config;
        public readonly Vector3 Position;

        public LootSpawnEntry(CollectableConfig config, Vector3 position)
        {
            Position = position;
            Config = config;
        }
    }
}