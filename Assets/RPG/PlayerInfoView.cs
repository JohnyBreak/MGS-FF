using System;
using RPG.Enums;
using TMPro;
using UI.MVP;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class PlayerInfoView : ViewBase
    {
        // для автоматизации
        // можно сделать лэйаут группу со статами
        // и при создании им будет прокидываться tatsType
        // кнопка апгрейда будет посылать тип
        
        // а можно просто захаркодить вьюшку на каждую стату
        
        [SerializeField] private TMP_Text _lvlText;
        [SerializeField] private TMP_Text _xpText;
        [SerializeField] private TMP_Text _nextLvlXpText;
        [SerializeField] private TMP_Text _skillStartText;
        [SerializeField] private TMP_Text _skillCurrentText;
        [SerializeField] private TMP_Text _upgradePointsText;
        [SerializeField] private Button _upgradeButton;

        public event Action<StatsTypes> UpgradeButton;
        
        public void SetXPText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _xpText.text = xpText;
        }
        
        public void SetNextXPText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _nextLvlXpText.text = xpText;
        }
        
        public void SetLVLText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _lvlText.text = xpText;
        }
        
        public void SetUpgradePointsText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _upgradePointsText.text = xpText;
        }
        
        public void SetStartSkillText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _skillStartText.text = xpText;
        }
        
        public void SetCurrentSkillText(string xpText)
        {
            if (!_xpText)
            {
                return;
            }

            _skillCurrentText.text = xpText;
        }

        public void ToggleUpgradeButton(bool isActive)
        {
            _upgradeButton.gameObject.SetActive(isActive);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        
        
        protected override void OnDispose()
        {
            base.OnDispose();
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }

        private void OnUpgradeButtonClicked()
        {
            UpgradeButton?.Invoke(StatsTypes.Strength);
        }
    }
}