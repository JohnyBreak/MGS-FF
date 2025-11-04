using DG.Tweening;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class BrokenState : IState
    {
        private Transform _rotator;
        private readonly DamageComposite _damageComposite;

        public BrokenState(Transform rotator, DamageComposite damageComposite)
        {
            _rotator = rotator;
            _damageComposite = damageComposite;
        }

        public void Enter()
        {
            _damageComposite.Toggle(false);
            
            var angles = _rotator.localEulerAngles;
            angles.x = 80;
            _rotator.DOLocalRotate(angles, .2f).SetEase(Ease.Linear);
        }

        public void Exit()
        {
            _damageComposite.Toggle(true);
        }

        public int GetKey()
        {
            return SCStateKeys.Broken;
        }
        
        public void Dispose()
        {
        }
    }
}