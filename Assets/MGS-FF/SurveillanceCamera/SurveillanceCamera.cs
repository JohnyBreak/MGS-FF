using System;
using System.Collections;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class SurveillanceCamera : MonoBehaviour
    {
        public event Action TargetSpottedEvent;
        public event Action TargetLostEvent;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _rotator;

        private Detection _detection;
        private RotationState _rotationState;
        private LookAtState _lookAtState;
        private BrokenState _brokenState;
        private Coroutine _resetRoutine;

        private void Awake()
        {
            _lookAtState = new LookAtState(_rotator, 65, 3);
            _detection = new Detection(_layerMask, _collider, OnTarget);
            _rotationState = new RotationState(_rotator, 4f, 65);
            _brokenState = new(transform, _rotator, OnBreak);
        }

        private IEnumerator Start()
        {
            yield return null;
            _detection.Init();
            _rotationState.Init();
            _brokenState.Init();
            _rotationState.Enable();
        }

        private void OnTarget(Transform target)
        {
            if (_resetRoutine != null)
            {
                StopCoroutine(_resetRoutine);
            }
            
            if (target == null)
            {
                _lookAtState.Disable();
                TargetLostEvent?.Invoke();
                _resetRoutine = StartCoroutine(ResetRoutine());
                return;
            }
            
            _rotationState.Disable();
            _lookAtState.Enable(target);
            TargetSpottedEvent?.Invoke();
        }

        private IEnumerator ResetRoutine()
        {
            yield return new WaitForSeconds(3);
            ResetState();
        }

        private void OnBreak()
        {
            _rotationState.Disable();
            _lookAtState.Disable();
            _detection.Disable();
            _brokenState.Enable();
        }

        private void ResetState()
        {
            _lookAtState.Disable();
            _detection.Enable();
            _brokenState.Disable();
            _rotationState.Enable();
        }

        private void OnDestroy()
        {
            _detection?.Dispose();
            _rotationState?.Dispose();
            _brokenState?.Dispose();
        }
    }
}