using UnityEngine;

namespace Sensors
{
    public class SightSensorBehaviour : MonoBehaviour
    {
        [SerializeField] private float _distance;
        [SerializeField] private float _angle;
        [SerializeField] private float _closeBottomHeight;
        [SerializeField] private float _closeTopHeight;
        [SerializeField] private float _farBottomHeight;
        [SerializeField] private float _farTopHeight;
        [SerializeField] private Color _color;
        [SerializeField] private LayerMask _targetMask;
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private int _pointsAmount;
        [SerializeField] private bool _draw;
        
        private SightSensorMesh _sensorMesh;
        private SightSensorScanner _scanner;
        
        private void Start()
        {
            _sensorMesh = new SightSensorMesh(
                _distance,
                _angle,
                _closeBottomHeight,
                _closeTopHeight,
                _farBottomHeight,
                _farTopHeight);

            _scanner = new SightSensorScanner(
                _sensorMesh,
                transform,
                _targetMask,
                _obstacleMask,
                _pointsAmount,
                _color);
        }

        private void Update()
        {
            _scanner.Tick();
        }

        private void OnValidate()
        {
            if (_sensorMesh == null)
            {
                return;
            }

            _sensorMesh.UpdateMesh(
                _distance,
                _angle,
                _closeBottomHeight,
                _closeTopHeight,
                _farBottomHeight,
                _farTopHeight);
        }

        private void OnDrawGizmos()
        {
            if (!_draw)
            {
                return;
            }

            if (_scanner != null)
            {
                _scanner.DrawGizmos();    
            }
        }
    }
}