using UnityEngine;

namespace UnitStateMachine
{
    public class IdleState : BaseState
    {
        public IdleState(StateMachine currentContext, StateFactory unitStateFactory)
            : base(currentContext, unitStateFactory)
        {
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
            if (Input.GetKeyDown(KeyCode.M))
            {
                SwitchState(_factory.Get(States.Move));
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}