using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField] private DialogueView _view;
        [SerializeField] private DialogueInteractable _interactable;
        [SerializeField] private GameObject _dialogueCamera;
        
        private DialogueManager _dialogueManager;
        private DialogueNodesContainer _testNodesContainer;

        private void Awake()
        {
            GameState.GameState.SetState(GameState.GameState.State.GamePlay);
        }

        void Start()
        {
            _testNodesContainer = new DialogueNodesContainer();
            
            _testNodesContainer.Append(new ShowTextDialogueNode("Hi! i'm Cube"));
            _testNodesContainer.Append(new ShowTextDialogueNode("Wow!"));
            _testNodesContainer.Append(new ShowTextDialogueNode("Yeah"));
            
            _dialogueManager = new DialogueManager(_view, _dialogueCamera);
            _interactable.Init(_dialogueManager, _testNodesContainer);
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