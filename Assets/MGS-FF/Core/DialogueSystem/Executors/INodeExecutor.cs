namespace DialogueSystem
{
    public interface INodeExecutor
    {
        public void Execute(BaseDialogueNode node);
    }

    public abstract class BaseExecutor<TNode> : INodeExecutor where TNode : BaseDialogueNode
    {
        public void Execute(BaseDialogueNode node)
        {
            OnExecute((TNode)node);
        }

        protected abstract void OnExecute(TNode node);
    }
}

