namespace AlertSystem
{
    public class JammingState : IState
    {
        private readonly AlertContext _alertContext;
        private readonly AlertView _view;
        private readonly float _duration;
        
        public JammingState(AlertView view)
        {
            _view = view;
        }

        public JammingState(AlertContext context, AlertView alertView, int duration)
        {
            _alertContext = context;
            _view = alertView;
            _duration = duration;
        }

        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public int GetKey()
        {
            return AlertStateKeys.JammingState;
        }

        public void Dispose()
        {
            // TODO release managed resources here
        }
    }
}