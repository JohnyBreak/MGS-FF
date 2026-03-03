using System;

namespace DialogueSystem
{
    public interface INodeExecutionContext
    {
        public void Initialize(DialogueNodesContainer nodesContainer);
        public void NextStep();
        public event Action EndEvent;
    }
}