using Collectables;
using UnityEngine;

public class CollectorTrigger : MonoBehaviour
{
   
   [SerializeField] private LayerMask _layerMask;
   private void OnTriggerEnter(Collider other)
   {
      if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
      {
         return;
      }

      if (other.TryGetComponent<ICollectableView>(out var collectable))
      {
         collectable.Collect();
      }
   }
}
