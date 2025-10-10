using System;
using Sensors;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class Detection : IDisposable
    {
        private CompositeDisposable _disposable = new();
        private SightSensorBehaviour _sensorBehaviour;
        private readonly Action<Transform> _onTarget;
        private Coroutine _resetRoutine;
        private bool _state;
        
        public Detection(SightSensorBehaviour sensorBehaviour, Action<Transform> onTarget)
        {
            _sensorBehaviour = sensorBehaviour;
            _onTarget = onTarget;
            Init();
        }

        private void Init()
        {
            _sensorBehaviour.OnScanEvent += OnScan;
            // _sensorBehaviour
            //     .OnTriggerEnterAsObservable()
            //     .Subscribe(OnTriggerEnter)
            //     .AddTo(_disposable);
            // _sensorBehaviour
            //     .OnTriggerExitAsObservable()
            //     .Subscribe(OnTriggerExit)
            //     .AddTo(_disposable);
            Enable();
        }

        private void OnScan()
        {
            var oldState = _state;

            _state = _sensorBehaviour.Objects.Count < 1;

            if (oldState == _state)
            {
                return;
            }

            if (_state)
            {
                _onTarget?.Invoke(null);
                return;
            }
            _onTarget?.Invoke(_sensorBehaviour.Objects[0].transform);
        }

        public void Enable()
        {
            if (_sensorBehaviour)
            {
                _sensorBehaviour.enabled = true;
            }
        }

        public void Disable()
        {
            if (_sensorBehaviour)
            {
                _sensorBehaviour.enabled = false;
            }
        }
        
        public void Dispose()
        {
            _sensorBehaviour.OnScanEvent -= OnScan;
            _disposable?.Dispose();
        }
    }
}