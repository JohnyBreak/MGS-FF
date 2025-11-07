using UnityEngine;

namespace UnitStateMachine
{
    public class MoveState : BaseState
    {
        public MoveState(StateMachine currentContext, StateFactory unitStateFactory)
            : base(currentContext, unitStateFactory)
        {
        }

        public override int Key()
        {
            return States.Move;
        }

        public override void EnterState()
        {
            Debug.Log("Enter MoveState");
        }

        public override void UpdateState()
        {
        }

        public override void ExitState()
        {
            Debug.Log("Exit MoveState");
        }

        public override void CheckSwitchState()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                SwitchState(_factory.Get(States.Idle));
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}