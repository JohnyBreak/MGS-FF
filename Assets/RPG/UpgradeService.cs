using RPG.Enums;
using UniRx;

namespace RPG
{
    public class UpgradeService
    {
        public IReactiveProperty<bool> CanUpgrade { get; } = new ReactiveProperty<bool>(false);
        private readonly CompositeDisposable _cd = new();
        private readonly PlayerXPModel _xpModel;
        private readonly PlayerLVLModel _lvlModel;
        private readonly UnitStatsModel _statsModel;

        public UpgradeService(
            PlayerXPModel xpModel,
            PlayerLVLModel lvlModel,
            UnitStatsModel statsModel)
        {
            _xpModel = xpModel;
            _lvlModel = lvlModel;
            _statsModel = statsModel;
        }

        public void Initialize()
        {
            _xpModel.CurrentXP.Subscribe(OnXpChanged).AddTo(_cd);
            _lvlModel.CurrentUpgradePoints.Subscribe(OnUPChanged).AddTo(_cd);
        }
        
        public bool TryUpgradeSkill(StatsTypes type)
        {
            if (_lvlModel.TryReduceUpgradePoints(1))
            {
                _statsModel.RegisterStat(type, _statsModel.GetValue(type) + 1);
                return true;
            }

            return false;
        }
        
        private void OnXpChanged(int newValue)
        {
            if (newValue < _xpModel.NextLvlXp.Value)
            {
                return;
            }

            LvlUp();
            CalculateNextLvlXp();
        }
        
        private void OnUPChanged(int newValue)
        {
            CanUpgrade.Value = newValue > 0;
        }
        
        private void LvlUp()
        {
            _lvlModel.AddLVL(1);
            _lvlModel.AddUpgradePoints(5);
        }

        private void CalculateNextLvlXp()
        {
            _xpModel.SetNextLvlXp(_xpModel.NextLvlXp.Value + _lvlModel.CurrentLVL.Value * 100);
        }
    }
}

