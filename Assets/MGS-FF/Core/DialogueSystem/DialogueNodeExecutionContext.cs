using System;
using System.Collections.Generic;
using System.Linq;

namespace DialogueSystem
{
    public class DialogueNodeExecutionContext : INodeExecutionContext
    {
        public event Action EndEvent;
        
        private DialogueNodesContainer _currentNodesContainer;
        private int _currentStep;
        private Dictionary<Type, INodeExecutor> _executors;
        
        public void RegisterExecutors(Dictionary<Type, INodeExecutor> executors)
        {
            _executors = executors;
        }

        public void Initialize(DialogueNodesContainer nodesContainer)
        {
            _currentNodesContainer = nodesContainer;
            _currentStep = -1;
        }

        public void NextStep()
        {
            if (_currentNodesContainer == null)
            {
                return;
            }

            _currentStep++;
            
            var nodes = _currentNodesContainer.Nodes;
            
            if (_currentStep >= nodes.Count)
            {
                EndEvent?.Invoke();
                return;
            }

            var node = nodes.ElementAt(_currentStep);
            
            if (!_executors.TryGetValue(node.GetType(), out var executor))
            {
                return;
            }
            executor.Execute(node, this);
        }
    }
}