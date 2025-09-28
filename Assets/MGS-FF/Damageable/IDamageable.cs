using System;

namespace DamageSystem
{
    public interface IDamageable
    {
        event Action<DamageInfo> DamagedEvent;
        
        void TakeDamage(DamageInfo damage);
        void Toggle(bool isActive);
    }
}
