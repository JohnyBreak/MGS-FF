using UI.MVP;
using UniRx;

namespace RPG
{
    public class PlayerInfoModel : ModelBase
    {
        private readonly PlayerXPModel _xpModel;
        private readonly PlayerLVLModel _lvlModel;
        private readonly UnitStatsModel _statsModel;
        
        public PlayerLVLModel LvlModel => _lvlModel;
        public PlayerXPModel XpModel => _xpModel;
        public UnitStatsModel StatsModel => _statsModel;
        
        //public IReactiveProperty<int> CurrentXP => _xpModel.CurrentXP;
        //public IReactiveProperty<int> NextLvlXp => _xpModel.NextLvlXp;
        //public IReactiveProperty<int> CurrentLVL => _lvlModel.CurrentLVL;
        //public IReactiveProperty<int> CurrentUpgradePoints => _lvlModel.CurrentUpgradePoints;
        //public IReactiveProperty<int> StartSkill => _statsModel.StartValue;
        //public IReactiveProperty<int> CurrentSkill => _statsModel.FinalValue;
        
        public PlayerInfoModel(
            PlayerXPModel xpModel, 
            PlayerLVLModel lvlModel,
            UnitStatsModel statsModel)
        {
            _xpModel = xpModel;
            _lvlModel = lvlModel;
            _statsModel = statsModel;
            
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();
        }

        protected override void OnDispose()
        {
            base.OnDispose();
        }
    }
}


