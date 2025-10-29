namespace Collectables
{
    public interface ICollector
    {
        bool TryCollect(ICollectableObject collectable);
        int GetCollectableType();
    }
}


