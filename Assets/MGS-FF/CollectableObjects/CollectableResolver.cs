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
        
        public bool TryCollect(ICollectableObject collectable)
        {
            if (collectable == null)
            {
                Debug.LogError($"Collectable is null");
                return false;
            }

            if (_collectorsMap.TryGetValue(collectable.GetCollectableType(), out var collector))
            {
                return collector.TryCollect(collectable);
            }
            
            Debug.LogError($"No collector for '{collectable.GetCollectableType()}' CollectableType");
            return false;
        }
    }
}