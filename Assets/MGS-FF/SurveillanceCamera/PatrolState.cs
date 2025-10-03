using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class PatrolState : IState
    {
        private Transform _rotator;
        private float _time;
        private Vector3 _left;
        private Vector3 _right;
        private Sequence _sequence;
        private float _halfAngle;
        private TweenerCore<Quaternion, Vector3, QuaternionOptions> _rot;

        public PatrolState(Transform rotator, float time, float halfAngle)
        {
            _rotator = rotator;
            _time = time;
            _halfAngle = halfAngle;
            
            _left = new Vector3(_rotator.localEulerAngles.x, -_halfAngle, _rotator.localEulerAngles.z);
            _right = new Vector3(_rotator.localEulerAngles.x, _halfAngle, _rotator.localEulerAngles.z);
        }

        public void Enter()
        {
            var value = Calculate(NormalizeAngle(_rotator.localEulerAngles.y));
            _rot = _rotator
                .DOLocalRotate(_left, _time * value)
                .SetEase(Ease.Linear)
                .OnComplete(StartLoop);
        }
        
        private void StartLoop()
        {
            _sequence = DOTween.Sequence();
            _sequence.AppendInterval(2);
            _sequence.Append(_rotator.DOLocalRotate(_right, _time)).SetEase(Ease.Linear);
            _sequence.AppendInterval(2);
        
            _sequence.Append(_rotator.DOLocalRotate(_left, _time)).SetEase(Ease.Linear);
            _sequence.SetLoops(-1);
        }
        
        private float NormalizeAngle(float angle)
        {
            while (angle > 180f) angle -= 360f;
            while (angle < -180f) angle += 360f;
            return angle;
        }
    
        private float Calculate(float angle)
        {
            float all = _halfAngle * 2;
            angle += _halfAngle;
            return angle / all;
        }
        
        public int GetKey()
        {
            return SCStateKeys.Patrol;
        }
        
        public void Exit()
        {
            _rot.Kill();
            _sequence.Kill();
        }
        
        public void Dispose()
        {
            Exit();
        }
    }
}