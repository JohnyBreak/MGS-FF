using SurveillanceCameraSystem;
using UnityEngine;

namespace AlertSystem
{
    public class AlertTest : MonoBehaviour
    {
        [SerializeField] private SurveillanceCamera[] _cameras;

        [SerializeField] private AlertView _alertView;
        private Alert _alert;
        
        private void Start()
        {
            _alert = new Alert(_alertView);

            foreach (var cam in _cameras)
            {
                cam.TargetSpottedEvent += OnSpot;
                cam.TargetLostEvent += OnLost;
            }
        }

        private void OnSpot()
        {
            _alert.Increase();
        }
        
        private void OnLost()
        {
            _alert.Decrease();
        }

        private void OnDestroy()
        {
            foreach (var cam in _cameras)
            {
                cam.TargetSpottedEvent -= OnSpot;
                cam.TargetLostEvent -= OnLost;
            }
        }
    }
}