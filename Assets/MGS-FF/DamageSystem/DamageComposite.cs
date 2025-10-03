using System;
using DamageSystem;
using UnityEngine;

namespace SurveillanceCameraSystem
{
    public class DamageComposite : IDisposable
    {
        private readonly Transform _parent;
        private readonly Action<IDamage> _onDamage;
        private IDamageable[] _damageables;
        
        public DamageComposite(Transform parent, Action<IDamage> onDamage)
        {
            _parent = parent;
            _onDamage = onDamage;
            Init();
        }

        private void Init()
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

        public void Toggle(bool isActive)
        {
            foreach (var damageable in _damageables)
            {
                damageable.Toggle(isActive);
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