using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField] private DialogueView _view;
        [SerializeField] private DialogueInteractable _interactable;
        [SerializeField] private GameObject _dialogueCamera;
        [SerializeField] private GameObject _player;
        
        private DialogueManager _dialogueManager;

        private void Awake()
        {
            GameState.GameState.SetState(GameState.GameState.State.GamePlay);
        }

        void Start()
        {
            _dialogueManager = new DialogueManager(_view, _dialogueCamera, _player);
            _interactable.Init(_dialogueManager);
        }

        void Update()
        {
            if (_dialogueManager.InProgress == false)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _dialogueManager.SkipOrNext();
            }
        }

        private void Callback()
        {
            Debug.LogError("dialogue end");
        }
    }
}