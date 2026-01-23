using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager
    {
        private readonly DialogueView _view;
        private GameObject _camera;
        private DialogueNodesContainer _currentNodesContainer;
        private int _currentStep;
        private Action _endCallback;
        private bool _inProgress;
        private Dictionary<Type, INodeExecutor> _executors;
        public bool InProgress => _inProgress;
        
        public DialogueManager(DialogueView view, GameObject camera)
        {
            _view = view;
            _camera = camera;
            camera.SetActive(false);
            _view.SetActive(false);

            _executors = new()
            {
                {typeof(ShowTextDialogueNode) ,new ShowTextNodeExecutor(_view)}
            };

        }

        public void StartDialogue(DialogueNodesContainer nodesContainer, Action endCallback = null)//dialogue data: camera settings, characters positions, text sequences
        {
            if (_inProgress)
            {
                return;
            }
            
            _inProgress = true;
            _currentNodesContainer = nodesContainer;
            _endCallback = endCallback;
            _currentStep = -1;
            _view?.Prepare();
            _view?.SetActive(true);
            _camera?.SetActive(true);
            
            GameState.GameState.SetState(GameState.GameState.State.Dialogue);
            
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
            var texts = _currentNodesContainer.Nodes;
            
            if (_currentStep >= texts.Count)
            {
                EndDialogue();
                return;
            }
            
            //_view.ShowPhraseImmediate(
            //   texts.ElementAt(_currentStep),
            //   () => _view.ToggleIndicator(true));
        }

        private void NextStep()
        {
            if (_currentNodesContainer == null || _view == null)
            {
                EndDialogue();
                return;
            }

            _currentStep++;
            
            var nodes = _currentNodesContainer.Nodes;
            
            if (_currentStep >= nodes.Count)
            {
                EndDialogue();
                return;
            }

            var node = nodes.ElementAt(_currentStep);
            var t = node.GetType();
            if (!_executors.TryGetValue(node.GetType(), out var executor))
            {
                return;
            }
            executor.Execute(node);
            //_view.ShowPhrase(
            //    nodes.ElementAt(_currentStep),
            //    () => _view.ToggleIndicator(false),
            //    () => _view.ToggleIndicator(true));
        }

        private void EndDialogue()
        {
            _view?.SetActive(false);
            _camera?.SetActive(false);
            
            _endCallback?.Invoke();
            _inProgress = false;
        }
    }
}