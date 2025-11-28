using System;
using System.Linq;

namespace DialogueSystem
{
    public class DialogueManager
    {
        private readonly DialogueView _view;
        private DialogueData _currentData;
        private int _currentStep;
        private Action _endCallback;
        private bool _inProgress;
        public DialogueManager(DialogueView view)
        {
            _view = view;
            
            _view.SetActive(false);
        }

        public void StartDialogue(DialogueData data, Action endCallback)//dialogue data: camera settings, characters positions, text sequences
        {
            if (_inProgress)
            {
                return;
            }

            _inProgress = true;
            _currentData = data;
            _endCallback = endCallback;
            _currentStep = -1;
            _view.Prepare();
            _view.SetActive(true);

            NextStep();
        }

        public void SkipOrNext()
        {
            if (_inProgress == false)
            {
                return;
            }

            if (_view.InProgress)
            {
                SkipAnim();
            }
            else
            {
                NextStep();
            }
        }

        private void SkipAnim()
        {
            var texts = _currentData.Texts;
            
            if (_currentStep >= texts.Count)
            {
                EndDialogue();
                return;
            }
            
            _view.ShowPhraseImmediate(
                texts.ElementAt(_currentStep),
                () => _view.ToggleIndicator(true));
        }

        private void NextStep()
        {
            if (_currentData == null || _view == null)
            {
                EndDialogue();
                return;
            }

            _currentStep++;
            
            var texts = _currentData.Texts;
            
            if (_currentStep >= texts.Count)
            {
                EndDialogue();
                return;
            }
            
            _view.ShowPhrase(
                texts.ElementAt(_currentStep),
                () => _view.ToggleIndicator(false),
                () => _view.ToggleIndicator(true));
        }

        private void EndDialogue()
        {
            _view?.SetActive(false);
            
            _endCallback?.Invoke();
            _inProgress = false;
        }
    }
}