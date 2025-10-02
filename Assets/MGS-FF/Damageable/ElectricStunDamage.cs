namespace DamageSystem
{
    public class ElectricStunDamage : IDamage
    {
        private readonly int _damageAmount;

        public ElectricStunDamage(int damageAmount)
        {
            _damageAmount = damageAmount;
        }

        public float GetDuration()
        {
            return _damageAmount;
        }

        public int GetAmount()
        {
            return _damageAmount;
        }

        public int GetDamageType()
        {
            return DamageTypes.ElectricStun;
        }
    }
}


