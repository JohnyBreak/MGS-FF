using System;
using UnityEngine;

namespace DamageSystem
{
    [RequireComponent(typeof(Collider))]
    public class DamageableCollider : MonoBehaviour, IDamageable
    {
        public event Action<IDamage> DamagedEvent;
        private Collider _collider;
        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

        public void TakeDamage(IDamage damage)
        {
            DamagedEvent?.Invoke(damage);
        }

        public void Toggle(bool isActive)
        {
            _collider.enabled = isActive;
        }
    }
}

