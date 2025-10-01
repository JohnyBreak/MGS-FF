using System;

namespace AlertSystem
{
    public class CalmState : IState
    {
        private readonly AlertView _view;
        private readonly AlertVision _vision;
        private readonly Action<int> _changeState;
        private bool _isActive;
        
        public CalmState(AlertView view, AlertVision vision, Action<int> changeState)
        {
            _view = view;
            _vision = vision;
            _changeState = changeState;
            _vision.FoundEvent += OnFound;
        }

        public void Enter()
        {
            _isActive = true;
            _view.SetState(GetKey());
        }

        public void Exit()
        {
            _isActive = false;
        }

        public int GetKey()
        {
            return AlertStateKeys.CalmState;
        }

        public void Dispose()
        {
            _vision.FoundEvent -= OnFound;
        }
        
        private void OnFound()
        {
            if (_isActive == false)
            {
                return;
            }
            
            _changeState?.Invoke(AlertStateKeys.AlertState);
        }
    }
}