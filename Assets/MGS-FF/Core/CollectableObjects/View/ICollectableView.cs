using Collectables.UI;

namespace Collectables.View
{
    public interface ICollectableView
    {
        void Init(CollectableResolver resolver, ICollectableObject collectable, CollectableFloatingTextCanvas canvas);
        void Collect();
    }
}