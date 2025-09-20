namespace Collectables
{
    public interface ICollector
    {
        void Collect(ICollectableObject collectable);
        int GetCollectableType();
    }
}


