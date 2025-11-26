using UnityEngine;

namespace InteractSystem
{
    public class DialogueIntaractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private string _text;
        
        public void Interact()
        {
            Debug.Log($"Interactable dialogue - {_text}");
        }
    }
}