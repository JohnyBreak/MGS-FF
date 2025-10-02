using System;
using System.Collections;
using DamageSystem;
using DG.Tweening;
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
        private DamageReceiver _damageReceiver;
        private Coroutine _resetRoutine;

        private void Awake()
        {
            _lookAtState = new LookAtState(_rotator, 65, 3);
            _detection = new Detection(_layerMask, _collider, OnTarget);
            _rotationState = new RotationState(_rotator, 4f, 65);
            _damageReceiver = new(transform, OnDamage);
        }

        private IEnumerator Start()
        {
            yield return null;
            _detection.Init();
            _rotationState.Init();
            _damageReceiver.Init();
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

        private void OnDamage(IDamage damage)
        {
            if (damage is ElectricStunDamage electricStunDamage)
            {
                OnStun(electricStunDamage.GetDuration());
                return;
            }

            OnBreak();
        }

        private void OnStun(float duration)
        {
            _rotationState.Disable();
            _lookAtState.Disable();
            _detection.Disable();
            _damageReceiver.Disable();

            _rotator.DOShakeRotation(duration, 45).OnComplete(_rotationState.Enable);
        }
 
        private void OnBreak()
        {
            var angles = _rotator.localEulerAngles;
            angles.x = 80;
            _rotator.DOLocalRotate(angles, .2f).SetEase(Ease.Linear);
            
            _rotationState.Disable();
            _lookAtState.Disable();
            _detection.Disable();
            _damageReceiver.Enable();
        }

        private void ResetState()
        {
            _lookAtState.Disable();
            _detection.Enable();
            _damageReceiver.Disable();
            _rotationState.Enable();
        }

        private void OnDestroy()
        {
            _detection?.Dispose();
            _rotationState?.Dispose();
            _damageReceiver?.Dispose();
        }
    }
}