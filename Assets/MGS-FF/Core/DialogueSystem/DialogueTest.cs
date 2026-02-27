using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField] private DialogueView _view;
        [SerializeField] private DialogueInteractable _interactable;
        [SerializeField] private GameObject _dialogueCamera;
        [SerializeField] private GameObject _player;
        
        private DialogueNodeExecutionContext _dialogueNodeExecutionContext;
        private DialogueController _controller;
        
        private void Awake()
        {
            GameState.GameState.SetState(GameState.GameState.State.GamePlay);
        }

        void Start()
        {
            var presenter = new DialoguePresenter(new DialogueModel(), _view);
            
            _dialogueNodeExecutionContext = new DialogueNodeExecutionContext();
            
            _controller = new DialogueController(_dialogueNodeExecutionContext, presenter);
            
            _dialogueCamera.SetActive(false);
            
            _dialogueNodeExecutionContext.RegisterExecutors(new()
            {
                {typeof(ShowTextDialogueNode) ,new ShowTextNodeExecutor(_controller)},
                {typeof(SetDialogueCameraNode) ,new SetDialogueCameraNodeExecutor(_dialogueCamera.transform)},
                {typeof(ToggleCameraNode) ,new ToggleCameraNodeExecutor(_dialogueCamera)},
                {typeof(SetPlayerNode) ,new SetPlayerNodeExecutor(_player.transform)},
            });
            
            _interactable.Init(_controller);
        }

        void Update()
        {
            if (_controller.InProgress == false)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _controller.NextClick();
            }
        }

        private void Callback()
        {
            Debug.LogError("dialogue end");
        }

        private void OnDestroy()
        {
            _controller.Dispose();
        }
    }
}