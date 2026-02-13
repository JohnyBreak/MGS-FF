using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _indicator;
        [SerializeField] private float _delay = 0.3f;
        
        private WaitForSeconds _delayWait; 
        private Coroutine _coroutine;
        private INodeExecutionContext _context;
        private Action _endCallback;
        public bool InProgress { get; private set; }

        private void Start()
        {
            _delayWait = new WaitForSeconds(_delay);
        }

        public void Init(INodeExecutionContext context)
        {
            _context = context;
        }

        public void SetActive(bool active)
        {
            _canvas.enabled = active;
        }

        public void Prepare()
        {
            StopRoutine();
            _text.text = String.Empty;
            _endCallback = null;
            InProgress = false;
            ToggleIndicator(false);
        }
        
        public void ToggleIndicator(bool active)
        {
            _indicator.SetActive(active);
        }

        public void MoveNext()
        {
            if (InProgress)
            {
                Skip();
                return;
            }
            _context?.MoveNext();
        }
        
        public void ShowPhrase(string text, Action prepCallback = null, Action endCallback = null)
        {
            StopRoutine();
            _coroutine = StartCoroutine(ShowRoutine(text, prepCallback, endCallback));
        }
        
        public void Skip()
        {
            StopRoutine();
            _text.maxVisibleCharacters = _text.textInfo.characterCount;
            _endCallback?.Invoke();
            _endCallback = null;
            InProgress = false;
        }
        
        private IEnumerator ShowRoutine(string text, Action prepCallback = null, Action endCallback = null)
        {
            InProgress = true;
            prepCallback?.Invoke();
            _endCallback = endCallback;
            _text.text = text;
            _text.maxVisibleCharacters = 0;

            _text.ForceMeshUpdate();
            int total = _text.textInfo.characterCount;

            for (int i = 0; i <= total; i++)
            {
                _text.maxVisibleCharacters = i;
                yield return _delayWait;
            }
            endCallback?.Invoke();
            _endCallback = null;
            InProgress = false;
        }

        private void StopRoutine()
        {
            if (_coroutine == null)
            {
                return;
            }
            
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}

