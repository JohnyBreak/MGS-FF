using RPG.Enums;

namespace RPG
{
    public readonly struct StatInfo
    {
        public readonly StatsTypes Type;
        public readonly int BaseValue;
        public readonly int CurrentValue;

        public StatInfo(StatsTypes type, int baseValue, int currentValue)
        {
            Type = type;
            BaseValue = baseValue;
            CurrentValue = currentValue;
        }
    }
}