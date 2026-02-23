using System.Collections.Generic;
using RPG.Enums;

namespace RPG
{
    public class StatCalculator
    {
        private Dictionary<StatsTypes, int> _additionalValues = new()
        {
            { StatsTypes.None, 0},
            {StatsTypes.Strength, 2}
        };
        
        public int Calculate(StatsTypes stat, int baseValue)
        {
            if (_additionalValues.TryGetValue(stat, out var addValue))
            {
                return baseValue + addValue;
            }

            return 0;
        }
    }
}