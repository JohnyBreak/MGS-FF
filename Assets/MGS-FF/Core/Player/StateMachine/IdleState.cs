using UnityEngine;

namespace UnitStateMachine
{
    public class IdleState : BaseState
    {
        private readonly PlayerInfoContainer _container;

        public IdleState(
            StateMachine currentContext, 
            StateFactory unitStateFactory, 
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
        }

        public override int Key()
        {
            return States.Idle;
        }

        public override void EnterState()
        {
            _container.MoveVector = Vector3.zero;
        }

        public override void OnUpdateState()
        {
        }

        public override void ExitState()
        {
        }

        public override void CheckSwitchState()
        {
            if (_container.MoveVector != Vector3.zero)
            {
                SwitchState(_factory.Get(States.Move));
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}