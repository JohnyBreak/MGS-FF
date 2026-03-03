namespace DialogueSystem
{
    public class ShowTextNodeExecutor : BaseExecutor<ShowTextDialogueNode>
    {
        private readonly DialogueController _dialogueController;
        public ShowTextNodeExecutor(DialogueController dialogueController)
        {
            _dialogueController = dialogueController;
        }

        protected override void OnExecute(ShowTextDialogueNode node, INodeExecutionContext context)
        {
            _dialogueController.ShowPhrase(node.Text);
        }
    }
}

