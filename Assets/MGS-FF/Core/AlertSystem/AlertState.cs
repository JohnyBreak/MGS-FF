using System;
using DG.Tweening;

namespace AlertSystem
{
    public class AlertState : IState
    {
        private readonly AlertView _view;
        private readonly AlertVision _vision;
        private readonly float _duration;
        private readonly Action<int> _changeState;
        private Tweener _timer;
        private bool _isActive;
        
        public AlertState(AlertView view, AlertVision vision, float duration, Action<int> changeState)
        {
            _view = view;
            _vision = vision;
            _duration = duration;
            _changeState = changeState;
            _vision.FoundEvent += Restart;
            _vision.LostEvent += StartTimer;
        }

        public void Enter()
        {
            _isActive = true;
            _view.SetState(GetKey());
            _view.SetText(_duration);
        }

        public void Exit()
        {
            _isActive = false;
            _timer?.Kill();
        }

        public int GetKey()
        {
            return AlertStateKeys.AlertState;
        }

        public void Dispose()
        {
            _timer?.Kill();
            
            _vision.FoundEvent -= StartTimer;
            _vision.FoundEvent -= Restart;
        }

        private void Restart()
        {
            if (_isActive == false)
            {
                return;
            }
            
            _timer?.Kill();
            Enter();
        }

        private void StartTimer()
        {
            if (_isActive == false)
            {
                return;
            }

            _timer = DOVirtual.Float(_duration, 0, _duration, (value) => _view.SetText(value))
                .SetEase(Ease.Linear)
                .OnComplete(() => _changeState?.Invoke(AlertStateKeys.EvasionState));
        }
    }
}