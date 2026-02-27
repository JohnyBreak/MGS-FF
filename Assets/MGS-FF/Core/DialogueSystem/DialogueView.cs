using System;
using System.Collections;
using TMPro;
using UI.MVP;
using UnityEngine;

namespace DialogueSystem
{
    public class DialogueView : ViewBase
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _indicator;
        [SerializeField] private float _delay = 0.3f;
        
        private WaitForSeconds _delayWait; 
        private Coroutine _coroutine;
        private Action _endCallback;
        public bool InProgress { get; private set; }

        private void Start()
        {
            _delayWait = new WaitForSeconds(_delay);
        }

        private void Prepare()
        {
            if (Canvas.enabled == false)
            {
                SetActive(true);
            }

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

        public void ShowPhrase(string text, Action prepCallback = null, Action endCallback = null)
        {
            Prepare();
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

