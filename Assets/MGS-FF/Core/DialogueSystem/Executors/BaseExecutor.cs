namespace DialogueSystem
{
    public interface INodeExecutor
    {
        public void Execute(BaseDialogueNode node, INodeExecutionContext context);
    }

    public abstract class BaseExecutor<TNode> : INodeExecutor where TNode : BaseDialogueNode
    {
        public void Execute(BaseDialogueNode node, INodeExecutionContext context)
        {
            OnExecute((TNode)node, context);
        }

        protected abstract void OnExecute(TNode node, INodeExecutionContext context);
    }
}

