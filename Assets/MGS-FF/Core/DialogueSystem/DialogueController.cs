using System;

namespace DialogueSystem
{
    public class DialogueController : IDisposable
    {
        private DialogueBuilder _dialogueBuilder;
        private readonly INodeExecutionContext _nodeExecutionContext;
        private DialoguePresenter _presenter;
        private Action _endCallback;
        private bool _inProgress;
        public bool InProgress => _inProgress;
        
        public DialogueController(INodeExecutionContext nodeExecutionContext, DialoguePresenter presenter)
        {
            _dialogueBuilder = new DialogueBuilder();
            _nodeExecutionContext = nodeExecutionContext;
            _nodeExecutionContext.EndEvent += EndDialogue;
            _presenter = presenter;
            _presenter.Initialize();
        }
        
        public void StartDialogue(string dialogueID, Action endCallback = null)
        {
            StartDialogue(_dialogueBuilder.GetDialogueByID(dialogueID), endCallback);
        }

        public void StartDialogue(DialogueNodesContainer nodesContainer, Action endCallback = null)
        {
            if (nodesContainer.IsEmpty)
            {
                return;
            }

            if (_inProgress)
            {
                return;
            }
            
            _inProgress = true;
            _nodeExecutionContext.Initialize(nodesContainer);
            _endCallback = endCallback;
            
            GameState.GameState.SetState(GameState.GameState.State.Dialogue);
            
            _nodeExecutionContext.NextStep();
        }

        public void NextClick()
        {
            MoveNext();
        }

        private void MoveNext()
        {
            if (_inProgress == false)
            {
                return;
            }

            if (_presenter.View.InProgress)
            {
                _presenter.View.Skip();
                return;
            }

            _nodeExecutionContext.NextStep();
        }
        
        public void ShowPhrase(string text)
        {
            _presenter.Model.Text.Value = text;
        }
        
        private void EndDialogue()
        {
            if (_inProgress == false)
            {
                return;
            }
            
            _presenter?.Stop();
            
            _endCallback?.Invoke();
            _inProgress = false;
        }

        public void Dispose()
        {
            _nodeExecutionContext.EndEvent -= EndDialogue;
        }
    }
}

