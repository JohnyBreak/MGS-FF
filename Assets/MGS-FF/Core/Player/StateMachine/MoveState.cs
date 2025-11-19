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
        }

        public override void OnUpdateState()
        {
            Move();
        }

        private void Move()
        {
            Vector3 forward = _container.CameraTransform.forward;
            Vector3 right = _container.CameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            Vector3 move = forward * _container.MoveVector.z + right * _container.MoveVector.x;

            if (_container.MoveVector.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move);
                _container.PlayerTransform.rotation = Quaternion.Slerp(_container.PlayerTransform.rotation, targetRotation, _container.RotationSpeed * Time.deltaTime);
                _container.DesiredMoveVector = move.normalized * (_container.MoveSpeed * Time.deltaTime);
                _container.CharacterController.Move(_container.DesiredMoveVector);
            }
        }

        public override void ExitState()
        {
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