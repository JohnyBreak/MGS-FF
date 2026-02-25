using RPG.Enums;
using UI.MVP;
using UniRx;

namespace RPG
{
    public class PlayerInfoPresenter : PresenterBase<PlayerInfoModel, PlayerInfoView>
    {
        private readonly UpgradeService _upgradeService;
        private readonly StatCalculator _calculator;
        private readonly CompositeDisposable _cd = new();
        public PlayerInfoPresenter(
            PlayerInfoModel model, 
            PlayerInfoView view, 
            UpgradeService upgradeService,
            StatCalculator calculator) : base(model, view)
        {
            _upgradeService = upgradeService;
            _calculator = calculator;
        }
        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            Model.XpModel.CurrentXP.Subscribe(OnXPChanged).AddTo(_cd);
            Model.XpModel.NextLvlXp.Subscribe(OnNextLvlXPChanged).AddTo(_cd);
            Model.LvlModel.CurrentLVL.Subscribe(OnLVLChanged).AddTo(_cd);
            Model.LvlModel.CurrentUpgradePoints.Subscribe(OnUPChanged).AddTo(_cd);
            
            Model.StatsModel.Stats.ObserveReplace()
                .Select(x => (x.Key, x.NewValue))
                .Subscribe(OnStatChanged).AddTo(_cd);

            View.UpgradeButton += OnUpgradeClick;
            _upgradeService.CanUpgrade.Subscribe(OnCanUpgrade).AddTo(_cd);

            UpdateView();
        }

        private void UpdateView()
        {
            OnXPChanged(Model.XpModel.CurrentXP.Value);
            OnNextLvlXPChanged(Model.XpModel.NextLvlXp.Value);
            OnLVLChanged(Model.LvlModel.CurrentLVL.Value);
            OnUPChanged(Model.LvlModel.CurrentUpgradePoints.Value);
            
            foreach (var pair in Model.StatsModel.Stats)
            {
                OnStatChanged((pair.Key, Model.StatsModel.GetValue(pair.Key)));
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _cd.Dispose();
            View.UpgradeButton -= OnUpgradeClick;
        }
        
        private void OnUpgradeClick(StatsTypes type)
        {
            _upgradeService.TryUpgradeSkill(type);
        }
        
        private void OnCanUpgrade(bool can)
        {
            View.ToggleUpgradeButton(can);
        }
        
        private void OnXPChanged(int newValue)
        {
            View.SetXPText(newValue.ToString());
        }
        
        private void OnNextLvlXPChanged(int newValue)
        {
            View.SetNextXPText(newValue.ToString());
        }
        
        private void OnLVLChanged(int newValue)
        {
            View.SetLVLText(newValue.ToString());
        }
        
        private void OnUPChanged(int newValue)
        {
            View.SetUpgradePointsText(newValue.ToString());
        }
        
        private void OnStatChanged((StatsTypes type, int newValue) pair)
        {
            View.SetBaseStatText(pair.type, pair.newValue.ToString());
            View.SetCurrentStatText(pair.type, _calculator.Calculate(pair.type, pair.newValue).ToString());
        }
    }
}