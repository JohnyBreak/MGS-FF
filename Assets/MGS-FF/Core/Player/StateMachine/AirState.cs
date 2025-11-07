using UnityEngine;

namespace UnitStateMachine
{
    public class AirState : BaseState
    {
        public AirState(StateMachine currentContext, StateFactory unitStateFactory)
            : base(currentContext, unitStateFactory)
        {
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
        }

        public override void ExitState()
        {
            Debug.Log("Exit Air");
        }

        public override void CheckSwitchState()
        {
            if (Input.GetKeyDown(KeyCode.G))
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