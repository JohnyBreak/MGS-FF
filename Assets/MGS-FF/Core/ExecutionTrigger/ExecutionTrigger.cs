using UnityEngine;

namespace ExecutionTriggers
{
    public class ExecutionTrigger : BaseExecutionTrigger
    {
        protected override void OnEnter(Collider other)
        {
            foreach (var executor in _executors)
            {
                executor.Execute();
            }
        }
    }
}