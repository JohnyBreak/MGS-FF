using InteractSystem;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        private DialogueManager _dialogueManager;
        private DialogueNodesContainer _nodesContainer;
        
        public void Init(DialogueManager dialogueManager, DialogueNodesContainer nodesContainer)
        {
            _dialogueManager = dialogueManager;
            _nodesContainer = nodesContainer;
        }

        public void Interact()
        {
            _dialogueManager.StartDialogue(_nodesContainer, () => { GameState.GameState.SetState(GameState.GameState.State.GamePlay);});
        }
    }
}