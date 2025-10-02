using System;

namespace DamageSystem
{
    public interface IDamageable
    {
        event Action<IDamage> DamagedEvent;
        
        void TakeDamage(IDamage damage);
        void Toggle(bool isActive);
    }
}
