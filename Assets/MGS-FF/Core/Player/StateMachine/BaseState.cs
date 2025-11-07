namespace UnitStateMachine
{
    public abstract class BaseState
    {
        protected bool _isRootState = false;
        protected StateMachine _ctx;
        protected StateFactory _factory;
        protected BaseState _currentSuperState;
        protected BaseState _currentSubState;

        public abstract int Key();

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract void CheckSwitchState();
        public abstract void InitializeSubState();

        public BaseState(StateMachine currentContext, StateFactory unitStateFactory)
        {
            _ctx = currentContext;
            _factory = unitStateFactory;
        }

        public void Update()
        {
            UpdateStates();
        }

        private void UpdateStates()
        {
            UpdateState();
            if (_currentSubState != null) _currentSubState.Update();
            CheckSwitchState();
        }
    
        protected void SwitchState(BaseState newState)
        {
            ExitState();

            newState.EnterState();
            if (_isRootState)
            {
                _ctx.SetState(newState);
            }
            else if (_currentSuperState != null)
            {
                _currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(BaseState newSuperState)
        {
            _currentSuperState = newSuperState;
        }

        protected void SetSubState(BaseState newSubState)
        {
            _currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

        public void ExitStates()
        {
            ExitState();
            if (_currentSubState != null) _currentSubState.ExitStates();
        }
    }
}