using System;
using System.Collections;
using UniRx;
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

        private CompositeDisposable _disposable = new();
        private Detection _detection;
        private RotationState _rotationState;
        private LookAtState _lookAtState;
        private BrokenState _brokenState;
        private Coroutine _resetRoutine;

        private void Awake()
        {
            _lookAtState = new LookAtState(_rotator, 65, 3);
            _detection = new Detection(_layerMask, _collider);
            _rotationState = new RotationState(_rotator, 4f, 65);
            
            _detection.Target
                .Skip(1)
                .Subscribe(OnTarget)
                .AddTo(_disposable);
            
            _brokenState = new(transform, _rotator);
            _brokenState.BreakEvent += OnBreak;
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
            if (target == null)
            {
                if (_resetRoutine != null)
                {
                    StopCoroutine(_resetRoutine);
                }
                
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
            _rotationState.Enable();
            _lookAtState.Disable();
        }

        private void OnBreak()
        {
            _rotationState.Disable();
            _lookAtState.Disable();
            _detection.Disable();
            _brokenState.Enable();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                OnBreak();
            }
            
            if (Input.GetKeyDown(KeyCode.K))
            {
                Reset();
            }
        }

        private void Reset()
        {
            _lookAtState.Disable();
            _detection.Enable();
            _brokenState.Disable();
            _rotationState.Enable();
        }

        private void OnDestroy()
        {
            if (_brokenState != null)
            {
                _brokenState.BreakEvent -= OnBreak;
            }
            _detection?.Dispose();
            _rotationState?.Dispose();
            _brokenState?.Dispose();
        }
    }
}