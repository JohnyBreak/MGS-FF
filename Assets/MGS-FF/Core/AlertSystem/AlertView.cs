using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AlertSystem
{
    public class AlertView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Image _image;

        public void Show() //??
        {
            
        }

        public void Hide()
        {
            
        }

        public void SetText(float value)
        {
            _text.text = $"{value}";
        }

        public void SetState(int alertStateKey)
        {
            _image.gameObject.SetActive(alertStateKey != AlertStateKeys.CalmState);
            
            switch (alertStateKey)
            {
                case AlertStateKeys.AlertState:
                    _image.color = Color.crimson;
                    break;
                case AlertStateKeys.EvasionState:
                    _image.color = Color.softYellow;
                    break;
                case AlertStateKeys.CalmState:
                    break;
            }
        }
    }
}