using System;
using DamageSystem;
using DG.Tweening;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class BrokenState : IDisposable
    {
        private readonly Transform _parent;
        private readonly Transform _head;
        private readonly Action _onBroke;
        private IDamageable[] _damageables;
        
        public BrokenState(Transform parent, Transform head, Action onBroke)
        {
            _parent = parent;
            _head = head;
            _onBroke = onBroke;
        }

        public void Init()
        {
            _damageables = _parent.GetComponentsInChildren<IDamageable>();
            foreach (var damageable in _damageables)
            {
                damageable.DamagedEvent += OnDamage;
            }
        }

        private void OnDamage(DamageInfo damageInfo)
        {
            //TODO: check if damage is valid
            _onBroke?.Invoke();
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