using RPG.Enums;
using UnityEngine;

namespace RPG
{
    public class RPGTest : MonoBehaviour
    {
        // сделать тестовую кнопку для начисления голды и опыта
        // сделать формулу для вычисления опыта для следующего уровня 
        
        [SerializeField] private PlayerInfoView _viewPrefab;
        [SerializeField] private RectTransform CanvasRect;
        
        private PlayerInfoView _view;
        private PlayerInfoModel _model;
        private PlayerInfoPresenter _presenter;
        private UpgradeService _upgradeService;
        private readonly PlayerXPModel _xpModel = new();
        private readonly PlayerLVLModel _lvlModel = new();
        private readonly UnitStatsModel _statsModel = new();
        
        private void Start()
        {
            _statsModel.RegisterStat(StatsTypes.Strength, 20);
            _statsModel.RegisterStat(StatsTypes.Magic, 10);
            _statsModel.RegisterStat(StatsTypes.Dexterity, 15);
            _statsModel.RegisterStat(StatsTypes.Vitality, 7);
            
            _upgradeService = new UpgradeService(_xpModel, _lvlModel, _statsModel);
            _upgradeService.Initialize();
            
            _view = Instantiate(_viewPrefab, CanvasRect);
            _view.Initialize();
            
            
            _model = new PlayerInfoModel(_xpModel, _lvlModel, _statsModel);
            _model.Initialize();
            _presenter = new PlayerInfoPresenter(
                _model, 
                _view, 
                _upgradeService,
                new StatCalculator());
            
            _presenter.Initialize();
        }

        [ContextMenu("AddXP")]
        private void AddXP()
        {
            _xpModel.AddXP(100);
        }
    }
}