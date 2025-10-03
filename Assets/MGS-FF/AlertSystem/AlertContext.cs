using System;
using System.Collections.Generic;

namespace AlertSystem
{
    public class AlertContext : IDisposable
    {
        private readonly AlertView _view;
        private readonly AlertVision _vision;
        private readonly Dictionary<int, IState> _statesMap = new();
        private IState _currentState;
        
        public AlertContext(AlertView view, AlertVision vision)
        {
            _view = view;
            _vision = vision;
            
            var alert = new AlertState(_view, _vision, 10, ChangeState);
            var evasion = new EvasionState(_view, _vision, 10, ChangeState);
            //var jamming = new JammingState(_view, 30);
            var calm = new CalmState(_view, _vision, ChangeState);
            
            _statesMap.Add(alert.GetKey(), alert);
            _statesMap.Add(evasion.GetKey(), evasion);
            //_statesMap.Add(jamming.GetKey(), jamming);
            _statesMap.Add(calm.GetKey(), calm);
            
            ChangeState(AlertStateKeys.CalmState);
        }

        private void ChangeState(int stateKey)
        {
            if (!_statesMap.ContainsKey(stateKey))
            {
                return;
            }

            _currentState?.Exit();

            _currentState = _statesMap[stateKey];
            
            _currentState.Enter();
        }

        public void Dispose()
        {
            foreach (var state in _statesMap.Values)
            {
                state.Dispose();
            }
        }
    }
}


