using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace DoorsSystem
{
    public class Door
    {
        private readonly CompositeDisposable _disposable;
        private readonly int _lvl;
        private readonly DoorView _view;
        // может потом переделать на компонент типа door acceptable
        // чтобы и нпс могли ходить через двери или перейти на тэги
        private readonly LayerMask _layerMask; 
        private readonly Func<int, bool> _validator;
        
        public Door(int lvl, DoorView view, LayerMask layerMask, Func<int, bool> validator)
        {
            _lvl = lvl;
            _view = view;
            _layerMask = layerMask;
            _validator = validator;
            _disposable = new();
        }

        public void Init()
        {
            if (_view == null)
            {
                Debug.LogError($"Door view us null");
                return;
            }
            
            if (_view.Collider == null)
            {
                Debug.LogError($"Door at position {_view.transform.position} Has no collider");
                return;
            }

            _view.Collider
                .OnTriggerEnterAsObservable()
                .Subscribe(OnTriggerEnter)
                .AddTo(_disposable);
            _view.Collider
                .OnTriggerExitAsObservable()
                .Subscribe(OnTriggerExit)
                .AddTo(_disposable);
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
            {
                return;
            }

            if (_lvl < 1)
            {
                _view.Open();
                return;
            }

            if (_validator.Invoke(_lvl) == false)
            {
                return;
            }
            
            _view.Open();
        }
        
        private void OnTriggerExit(Collider other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) <= 0)
            {
                return;
            }
            
            _view.Close();
        }
    }
}