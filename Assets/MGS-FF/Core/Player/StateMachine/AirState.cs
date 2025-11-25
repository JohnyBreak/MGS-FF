using UnityEngine;

namespace UnitStateMachine
{
    public class AirState : BaseState, IRootState
    {
        private readonly PlayerInfoContainer _container;

        public AirState(
            StateMachine currentContext, 
            StateFactory unitStateFactory,
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
        }

        public override int Key()
        {
            return States.Air;
        }

        public override void EnterState()
        {
            InitializeSubState();
        }

        public override void OnUpdateState()
        {
            _container.GroundCheck.Check();
            _container.YVector = Vector3.down * (_container.FallSpeed * Time.deltaTime);
            _container.YVector.z = 0;
            _container.YVector.x = 0;
            
            _container.CharacterController.Move(_container.YVector);
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchState()
        {
            _container.GroundCheck.Check();
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