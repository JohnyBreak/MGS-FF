using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTest : MonoBehaviour
    {
        [SerializeField] private DialogueView _view;

        private DialogueManager _dialogueManager;
        private DialogueData _testData;
        
        void Start()
        {
            _testData = new DialogueData();
            
            _testData.AppendText("first");
            _testData.AppendText("second");
            _testData.AppendText("third");
            
            _dialogueManager = new DialogueManager(_view);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                _dialogueManager.StartDialogue(_testData, Callback);
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