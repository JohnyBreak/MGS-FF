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

    public readonly struct LootSpawnEntry
    {
        public readonly int LootType;
        public readonly Vector3 Position;

        public LootSpawnEntry(int type, Vector3 position)
        {
            Position = position;
            LootType = type;
        }
    }
}