using UnityEngine;

namespace ExecutionTriggers
{
    public abstract class BaseExecutionTrigger : MonoBehaviour
    {
        [SerializeField] protected LayerMask _mask;
        [SerializeField] protected BaseTriggerExecutor[] _executors;
        
        private void OnTriggerEnter(Collider other)
        {
            if (((1 << other.gameObject.layer) & _mask) == 0)
            {
                return;
            }

            OnEnter(other);
        }

        protected abstract void OnEnter(Collider other);
    }
}