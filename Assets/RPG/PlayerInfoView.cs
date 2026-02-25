using System;
using System.Collections.Generic;
using System.Linq;
using RPG.Enums;
using TMPro;
using UI.MVP;
using UniRx;
using UnityEngine;

namespace RPG
{
    public class PlayerInfoView : ViewBase
    {
        // для автоматизации
        // можно сделать лэйаут группу со статами
        // и при создании им будет прокидываться tatsType
        // кнопка апгрейда будет посылать тип
        
        // а можно просто захаркодить вьюшку на каждую стату
        private CompositeDisposable _cd = new();
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private TMP_Text _xpText;
        [SerializeField] private TMP_Text _nextLvlXpText;
        [SerializeField] private TMP_Text _upgradePointsText;
        [SerializeField] private StatLineView[] _stats;
        
        private Dictionary<StatsTypes, StatLineView> _statsMap;
        
        public event Action<StatsTypes> UpgradeButton;

        public void UpdateStat(StatInfo info)
        {
            
        }

        public void SetXPText(string value)
        {
            if (!_xpText)
            {
                return;
            }

            _xpText.text = value;
        }
        
        public void SetNextXPText(string value)
        {
            if (!_nextLvlXpText)
            {
                return;
            }

            _nextLvlXpText.text = value;
        }
        
        public void SetLVLText(string value)
        {
            if (!_lvlText)
            {
                return;
            }

            _lvlText.text = value;
        }
        
        public void SetUpgradePointsText(string value)
        {
            if (!_upgradePointsText)
            {
                return;
            }

            

            _upgradePointsText.text = value;
        }
        
        public void SetBaseStatText(StatsTypes type, string value)
        {
            if (!_statsMap.TryGetValue(type, out var line))
            {
                return;
            }
            
            line.SetBaseValue(value);
        }
        
        public void SetCurrentStatText(StatsTypes type, string value)
        {
            if (!_statsMap.TryGetValue(type, out var line))
            {
                return;
            }
            
            line.SetCurrentValue(value);
        }

        public void ToggleUpgradeButton(bool isActive)
        {
            foreach (var line in _stats)
            {
                if (!line)
                {
                    continue;
                }

                line.ToggleUpgradeButton(isActive);
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _stats
                .Select(x => x.Button.OnClickAsObservable().Select(_ => x))
                .Merge()
                .Subscribe(OnUpgradeButtonClicked)
                .AddTo(_cd);

            _statsMap = new();
            foreach (var line in  _stats)
            {
                line.SetName(line.Type.ToString());
                _statsMap[line.Type] = line;
            }
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            _cd.Clear();
        }

        private void OnUpgradeButtonClicked(StatLineView line)
        {
            UpgradeButton?.Invoke(line.Type);
        }
    }
}