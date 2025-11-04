namespace Collectables
{
    public class PistolAmmoCollectable : ICollectableObject
    {
        private readonly int _amount;

        public PistolAmmoCollectable(int amount)
        {
            _amount = amount;
        }

        public int GetAmount()
        {
            return _amount;
        }
        
        public int GetCollectableType()
        {
            return CollectablesTypes.PistolAmmo;
        }
    }
}