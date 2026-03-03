using InteractSystem;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _dialogueID;
        
        private DialogueController _dialogueController;
        
        public void Init(DialogueController dialogueController)
        {
            _dialogueController = dialogueController;
        }

        public void Interact()
        {
            _dialogueController.StartDialogue(_dialogueID, () => { GameState.GameState.SetState(GameState.GameState.State.GamePlay);});
        }
    }
}