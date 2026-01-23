using System.Collections.Generic;

namespace DialogueSystem
{
    public class DialogueNodesContainer// рализовать ienumerable / ienumerator
    {
        private List<BaseDialogueNode> _nodes = new List<BaseDialogueNode>();

        public IReadOnlyCollection<BaseDialogueNode> Nodes => _nodes;
        
        public void Append(BaseDialogueNode text)
        {
            _nodes.Add(text);
        }
    }
}