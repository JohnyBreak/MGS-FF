namespace Collectables
{
    public class PistolAmmoCollector : ICollector
    {
        private readonly TestCollectableContainer _container;

        public PistolAmmoCollector(TestCollectableContainer container)
        {
            _container = container;
        }

        public void Collect(ICollectableObject collectable)
        {
            if (collectable is PistolAmmoCollectable ammo)
            {
                Collect(ammo);
            }
        }

        public int GetCollectableType()
        {
            return CollectablesTypes.PistolAmmo;
        }

        private void Collect(PistolAmmoCollectable pistolAmmo)
        {
            _container.PistolAmmo += pistolAmmo.GetAmount();
        }
    }
}

