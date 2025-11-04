using System;
using UniRx;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class FollowState : IState
    {
        private CompositeDisposable _disposable = new();
        private Transform _rotator;
        private Transform _target;
        private readonly Func<Transform> _targetGetter;
        private float _speed = 3f;
        private float _maxPitch = 55f;
        private float _minPitch = 0f;
        private float _minYaw = -65f;
        private float _maxYaw = 65f;

        public FollowState(Transform rotator, float halfAngle, float speed, Func<Transform> targetGetter)
        {
            _rotator = rotator;
            _speed = speed;
            _targetGetter = targetGetter;
            _minYaw = -halfAngle;
            _maxYaw = halfAngle;
        }

        public void Enter()
        {
            _target = _targetGetter?.Invoke();
            Observable.EveryUpdate()
                .Subscribe(_ => Update())
                .AddTo(_disposable);
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            Vector3 localTargetDir = _rotator.parent
                ? _rotator.parent.InverseTransformDirection(_target.position - _rotator.position)
                : _target.position - _rotator.position;

            if (localTargetDir == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(localTargetDir);

            Vector3 targetEuler = targetRotation.eulerAngles;

            targetEuler = NormalizeAngles(targetEuler);

            float deltaYaw = Mathf.Clamp(targetEuler.y, _minYaw, _maxYaw);
            float deltaPitch = Mathf.Clamp(targetEuler.x, _minPitch, _maxPitch);

            Vector3 clampedEuler = new Vector3(
                deltaPitch,
                deltaYaw,
                0f
            );

            Quaternion clampedRotation = Quaternion.Euler(clampedEuler);
            _rotator.localRotation = Quaternion.Slerp(_rotator.localRotation, clampedRotation, _speed * Time.deltaTime);

        }

        public void Exit()
        {
            _disposable.Clear();
            _target = null;
        }

        public int GetKey()
        {
            return SCStateKeys.Follow;
        }

        private Vector3 NormalizeAngles(Vector3 angles)
        {
            return new Vector3(
                NormalizeAngle(angles.x),
                NormalizeAngle(angles.y),
                NormalizeAngle(angles.z)
            );
        }

        private float NormalizeAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}