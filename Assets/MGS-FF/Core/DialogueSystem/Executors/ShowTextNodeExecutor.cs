namespace DialogueSystem
{
    public class ShowTextNodeExecutor : BaseExecutor<ShowTextDialogueNode>
    {
        private readonly DialogueView _view;
        public ShowTextNodeExecutor(DialogueView view)
        {
            _view = view;
        }

        protected override void OnExecute(ShowTextDialogueNode node)
        {
            _view.ShowPhraseImmediate(
                node.Text,
                () => _view.ToggleIndicator(true));
        }
    }
}

