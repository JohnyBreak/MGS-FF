using UnityEngine;

namespace UnitStateMachine
{
    public class GroundedState : BaseState
    {
        public GroundedState(StateMachine currentContext, StateFactory unitStateFactory)
            : base(currentContext, unitStateFactory)
        {
            _isRootState = true;
        }

        public override int Key()
        {
            return States.Grounded;
        }

        public override void EnterState()
        {
            InitializeSubState();
            Debug.Log("Enter GroundedState");
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Debug.Log("Exit GroundedState");
        }

        public override void CheckSwitchState()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SwitchState(_factory.Get(States.Air));
            }
        }

        public override void InitializeSubState()
        {
            SetSubState(_factory.Get(States.Idle));
        }
    }
}