using UnityEngine;

namespace UnitStateMachine
{
    public class AirState : BaseState
    {
        private readonly PlayerInfoContainer _container;

        public AirState(
            StateMachine currentContext, 
            StateFactory unitStateFactory,
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
            _isRootState = true;
        }

        public override int Key()
        {
            return States.Air;
        }

        public override void EnterState()
        {
            InitializeSubState();
            Debug.Log("Enter Air");
        }

        public override void UpdateState()
        {
            _container.PlayerTransform.Translate(Vector3.down * (_container.FallSpeed * Time.deltaTime));
        }

        public override void ExitState()
        {
            Debug.Log("Exit Air");
        }

        public override void CheckSwitchState()
        {
            if (_container.GroundCheck.IsGrounded)
            {
                SwitchState(_factory.Get(States.Grounded));
            }
        }

        public override void InitializeSubState()
        {
            SetSubState(_factory.Get(States.Idle));
        }
    }
}