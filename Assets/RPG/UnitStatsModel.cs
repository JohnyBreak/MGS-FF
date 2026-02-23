using System.Collections.Generic;
using System.Linq;
using RPG.Enums;
using UniRx;

namespace RPG
{
    public class UnitStatsModel
    {
        // надо ввести концепцию: есть базовый показатель статы, например, сила = 5
        // и итоговый показатель, который считается от вещей, бафов и прочего, например сила = 7(кольцо силы +2)
        
        // завести енум для типов статов и хранить словарь
        // словарь базовых значений, например, сила = 5
        // и сделать метод, который будет выдавать базовое значение по типу
        // так же сделать метод, который будет считать итоговое значение через StatCalculator(вынести в отдельное место)
        
        // для апгрейдов можно завести словарь стратегий с апгрейдерами: тип - апгрейдер
        // метод регистрации апгрейдов
        
        // сделать метод Upgrade и передавать туда тип и апгрейд (по базовому или интерфейсу)
        // находить по типу из словаря апгрейдер и передавать внутрь апгрейд
        // апгрейд будет приводиться в конкретный тип и будут проводиться манипуляции
        // возможно можно во время регистрации апгрейдера передать внутрь апгрейдера нужную стату через приватный метод выдачи статы через тип(обдумать)
        
        // всё задиспоузить
        // ?вынести апгрейд систему отдельно, чтобы она одна работала со всеми юнитами?
        private const int NoValue = -1;
        private ReactiveDictionary<StatsTypes, int> _stats = new();

        public ReactiveDictionary<StatsTypes, int> Stats => _stats;
        //public IReactiveProperty<int> StartValue { get; private set; } = new ReactiveProperty<int>(1);
        //public IReactiveProperty<int> FinalValue { get; private set; } = new ReactiveProperty<int>(1);

        public void RegisterStat(StatsTypes statType, int startValue)
        {
            if (!_stats.TryGetValue(statType, out var currentValue))
            {
                _stats[statType] = startValue;
                return;
            }

            if (currentValue >= startValue)
            {
                return;
            }
            
            _stats[statType] = startValue;
        }

        public int GetValue(StatsTypes statType)
        {
            return _stats.TryGetValue(statType, out var value) ? value : NoValue;
        }
    }
}