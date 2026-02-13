using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueManager : INodeExecutionContext
    {
        private readonly DialogueView _view;
        private DialogueNodesContainer _currentNodesContainer;
        private DialogueBuilder _dialogueBuilder;
        private int _currentStep;
        private Action _endCallback;
        private bool _inProgress;
        private Dictionary<Type, INodeExecutor> _executors;
        public bool InProgress => _inProgress;
        
        public DialogueManager(DialogueView view, GameObject camera, GameObject player)
        {
            _view = view;
            _view.Init(this);
            _view.SetActive(false);
            
            camera.SetActive(false);
            _dialogueBuilder = new DialogueBuilder();
            
            _executors = new()
            {
                {typeof(ShowTextDialogueNode) ,new ShowTextNodeExecutor(_view)},
                {typeof(SetDialogueCameraNode) ,new SetDialogueCameraNodeExecutor(camera.transform)},
                {typeof(ToggleCameraNode) ,new ToggleCameraNodeExecutor(camera)},
                {typeof(SetPlayerNode) ,new SetPlayerNodeExecutor(player.transform)},
            };
        }

        public void StartDialogue(string dialogueID, Action endCallback = null)
        {
            StartDialogue(_dialogueBuilder.GetDialogueByID(dialogueID), endCallback);
        }

        public void StartDialogue(DialogueNodesContainer nodesContainer, Action endCallback = null)//dialogue data: camera settings, characters positions, text sequences
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
            _currentNodesContainer = nodesContainer;
            _endCallback = endCallback;
            _currentStep = -1;
            _view?.Prepare();
            _view?.SetActive(true);
            
            GameState.GameState.SetState(GameState.GameState.State.Dialogue);
            
            NextStep();
        }

        public void MoveNext()
        {
            if (_inProgress == false)
            {
                return;
            }
            
            NextStep();
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
            
            if (!_executors.TryGetValue(node.GetType(), out var executor))
            {
                return;
            }
            executor.Execute(node, this);
        }

        private void EndDialogue()
        {
            _view?.SetActive(false);
            
            _endCallback?.Invoke();
            _inProgress = false;
        }
    }
}