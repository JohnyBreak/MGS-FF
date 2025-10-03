using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class Detection : IDisposable
    {
        private CompositeDisposable _disposable = new();
        private LayerMask _layerMask;
        private Collider _collider;
        private readonly Action<Transform> _onTarget;
        private Coroutine _resetRoutine;
        
        public Detection(LayerMask layerMask, Collider collider, Action<Transform> onTarget)
        {
            _layerMask = layerMask;
            _collider = collider;
            _onTarget = onTarget;
            Init();
        }

        private void Init()
        {
            _collider
                .OnTriggerEnterAsObservable()
                .Subscribe(OnTriggerEnter)
                .AddTo(_disposable);
            _collider
                .OnTriggerExitAsObservable()
                .Subscribe(OnTriggerExit)
                .AddTo(_disposable);
            Enable();
        }

        public void Enable()
        {
            _collider.enabled = true;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
            {
                return;
            }
            _onTarget?.Invoke(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
            {
                return;
            }
            
            _onTarget?.Invoke(null);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}