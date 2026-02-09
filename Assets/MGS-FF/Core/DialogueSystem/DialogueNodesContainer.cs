using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueNodesContainer// рализовать ienumerable / ienumerator
    {
        private List<BaseDialogueNode> _nodes = new List<BaseDialogueNode>();
        public bool IsEmpty => _nodes.Count < 1;
        public IReadOnlyCollection<BaseDialogueNode> Nodes => _nodes;
        
        public DialogueNodesContainer Append(BaseDialogueNode text)
        {
            _nodes.Add(text);
            return this;
        }

        public static DialogueNodesContainer GetEmpty()
        {
            return new DialogueNodesContainer();
        }
    }
}