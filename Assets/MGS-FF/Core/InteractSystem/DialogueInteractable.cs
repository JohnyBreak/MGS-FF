using InteractSystem;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _dialogueID;
        
        private DialogueManager _dialogueManager;
        
        public void Init(DialogueManager dialogueManager)
        {
            _dialogueManager = dialogueManager;
        }

        public void Interact()
        {
            _dialogueManager.StartDialogue(_dialogueID, () => { GameState.GameState.SetState(GameState.GameState.State.GamePlay);});
        }
    }
}