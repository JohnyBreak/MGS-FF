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

        private Coroutine _coroutine;
        public bool InProgress { get; private set; }

        public void SetActive(bool active)
        {
            _canvas.enabled = active;
        }

        public void Prepare()
        {
            ShowPhraseImmediate(string.Empty);
            ToggleIndicator(false);
        }
        
        public void ShowPhrase(string text, Action prepCallback = null, Action endCallback = null)
        {
            StopRoutine();
            _coroutine = StartCoroutine(ShowRoutine(text, prepCallback, endCallback));
        }
        
        public void ShowPhraseImmediate(string text, Action endCallback = null)
        {
            StopRoutine();
            _text.text = text;
            endCallback?.Invoke();
            InProgress = false;
        }
        
        private IEnumerator ShowRoutine(string text, Action prepCallback = null, Action endCallback = null)
        {
            InProgress = true;
            prepCallback?.Invoke();
            _text.text = "wait";
            
            yield return new WaitForSeconds(3);
            
            _text.text = text;
            endCallback?.Invoke();
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

        public void ToggleIndicator(bool active)
        {
            _indicator.SetActive(active);
        }
    }
}

