using System;
using DamageSystem;
using DG.Tweening;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class BrokenState : IDisposable
    {
        public event Action BreakEvent;
        private readonly Transform _parent;
        private readonly Transform _head;
        private IDamageable[] _damageables;
        
        public BrokenState(Transform parent, Transform head)
        {
            _parent = parent;
            _head = head;
        }

        public void Init()
        {
            // get hit boxes
            
            _damageables = _parent.GetComponentsInChildren<IDamageable>();
            foreach (var damageable in _damageables)
            {
                damageable.DamagedEvent += OnDamage;
            }
            // invoke break event
        }

        private void OnDamage(DamageInfo damageInfo)
        {
            //TODO: check if damage is valid
            BreakEvent?.Invoke();
        }

        public void Enable()
        {
            var angles = _head.localEulerAngles;
            angles.x = 80;
            _head.DOLocalRotate(angles, .2f).SetEase(Ease.Linear);
            
            foreach (var damageable in _damageables)
            {
                damageable.Toggle(false);
            }
        }

        public void Disable()
        {
            foreach (var damageable in _damageables)
            {
                damageable.Toggle(true);
            }
        }


        public void Dispose()
        {
            foreach (var damageable in _damageables)
            {
                damageable.DamagedEvent -= OnDamage;
            }
        }
    }
}