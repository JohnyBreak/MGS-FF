using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Collectables
{
    public class CollectableResolver
    {
        private readonly Dictionary<int, ICollector> _collectorsMap;
        
        public CollectableResolver(List<ICollector> collectors)
        {
            _collectorsMap = collectors.ToDictionary(x => x.GetCollectableType());
        }
        
        public void Collect(ICollectableObject collectable)
        {
            if (collectable == null)
            {
                Debug.LogError($"Collectable is null");
                return;
            }

            if (_collectorsMap.TryGetValue(collectable.GetCollectableType(), out var collector))
            {
                collector.Collect(collectable);
                return;
            }
            
            Debug.LogError($"No collector for '{collectable.GetCollectableType()}' CollectableType");
        }
    }
}