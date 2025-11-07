using UnityEngine;

namespace UnitStateMachine
{
    public class IdleState : BaseState
    {
        private readonly PlayerInfoContainer _infoContainer;

        public IdleState(
            StateMachine currentContext, 
            StateFactory unitStateFactory, 
            PlayerInfoContainer infoContainer)
            : base(currentContext, unitStateFactory)
        {
            _infoContainer = infoContainer;
        }

        public override int Key()
        {
            return States.Idle;
        }

        public override void EnterState()
        {
            Debug.Log("Enter Idle");
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Debug.Log("Exit Idle");
        }

        public override void CheckSwitchState()
        {
            if (_infoContainer.MoveVector != Vector3.zero)
            {
                SwitchState(_factory.Get(States.Move));
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}