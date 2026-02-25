using RPG.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG
{
    public class StatLineView : MonoBehaviour
    {
        [SerializeField] private StatsTypes _type;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _baseValue;
        [SerializeField] private TMP_Text _currentValue;
        [SerializeField] private Button _upgradeButton;
        
        public StatsTypes Type => _type;

        public Button Button => _upgradeButton;
    
        public void SetName(string value)
        {
            if (!_name)
            {
                return;
            }
            _name.text = value;
        }
    
        public void SetBaseValue(string value)
        {
            if (!_baseValue)
            {
                return;
            }
            _baseValue.text = value;
        }
    
        public void SetCurrentValue(string value)
        {
            if (!_currentValue)
            {
                return;
            }
            _currentValue.text = value;
        }
        
        public void ToggleUpgradeButton(bool isActive)
        {
            if (!_upgradeButton)
            {
                return;
            }

            _upgradeButton.gameObject.SetActive(isActive);
        }
    }
}

