using System;
using DG.Tweening;

namespace AlertSystem
{
    public class EvasionState : IState
    {
        private readonly AlertView _view;
        private readonly AlertVision _vision;
        private readonly float _duration;
        private readonly Action<int> _changeState;
        private Tweener _timer;

        public EvasionState(AlertView view, AlertVision vision, float duration, Action<int> changeState)
        {
            _view = view;
            _vision = vision;
            _duration = duration;
            _changeState = changeState;
            _vision.FoundEvent += OnFound;
        }

        public void Enter()
        {
            _view.SetState(GetKey());
            _timer = DOVirtual.Float(_duration, 0, _duration, (value) => _view.SetText(value))
                .SetEase(Ease.Linear)
                .OnComplete(() => _changeState?.Invoke(AlertStateKeys.CalmState));
        }

        public void Exit()
        {
            _timer?.Kill();
        }

        public int GetKey()
        {
            return AlertStateKeys.EvasionState;
        }

        public void Dispose()
        {
            _vision.FoundEvent -= OnFound;
            _timer?.Kill();
        }
        
        private void OnFound()
        {
            _changeState?.Invoke(AlertStateKeys.AlertState);
        }
    }
}