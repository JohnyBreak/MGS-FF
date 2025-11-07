using UnityEngine;

namespace UnitStateMachine
{
    public class MoveState : BaseState
    {
        private readonly PlayerInfoContainer _container;

        public MoveState(
            StateMachine currentContext, 
            StateFactory unitStateFactory,
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
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
            if (_container.MoveVector.magnitude > 0.1f)
            {
                _container.PlayerTransform.Translate(_container.MoveVector * (_container.MoveSpeed * Time.deltaTime));
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exit MoveState");
        }

        public override void CheckSwitchState()
        {
            if (_container.MoveVector == Vector3.zero)
            {
                SwitchState(_factory.Get(States.Idle));
            }
        }

        public override void InitializeSubState()
        {
        }
    }
}