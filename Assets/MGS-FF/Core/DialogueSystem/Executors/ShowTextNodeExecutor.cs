namespace DialogueSystem
{
    public class ShowTextNodeExecutor : INodeExecutor
    {
        private readonly DialogueView _view;
        public ShowTextNodeExecutor(DialogueView view)
        {
            _view = view;
        }

        public void Execute(BaseDialogueNode node)
        {
            _view.ShowPhraseImmediate(
                ((ShowTextDialogueNode)node).Text,
                () => _view.ToggleIndicator(true));
        }
    }
}

