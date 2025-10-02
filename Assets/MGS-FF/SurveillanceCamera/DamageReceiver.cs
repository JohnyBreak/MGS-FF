using System;
using DamageSystem;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class DamageReceiver : IDisposable
    {
        private readonly Transform _parent;
        private readonly Action<IDamage> _onDamage;
        private IDamageable[] _damageables;
        
        public DamageReceiver(Transform parent, Action<IDamage> onDamage)
        {
            _parent = parent;
            _onDamage = onDamage;
        }

        public void Init()
        {
            _damageables = _parent.GetComponentsInChildren<IDamageable>();
            foreach (var damageable in _damageables)
            {
                damageable.DamagedEvent += OnDamage;
            }
        }

        private void OnDamage(IDamage damageInfo)
        {
            _onDamage?.Invoke(damageInfo);
        }

        public void Enable()
        {
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