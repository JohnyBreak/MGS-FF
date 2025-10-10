using Sensors;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCameraView : MonoBehaviour
    {
        [SerializeField] private Transform _rotator;
        [SerializeField] private SightSensorBehaviour _sensorBehaviour;
        public Transform Rotator => _rotator;
        public SightSensorBehaviour SensorBehaviour => _sensorBehaviour;
    }
}

