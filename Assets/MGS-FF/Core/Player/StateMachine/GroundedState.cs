using UnityEngine;

namespace UnitStateMachine
{
    public class GroundedState : BaseState, IRootState
    {
        private readonly PlayerInfoContainer _container;

        public GroundedState(
            StateMachine currentContext, 
            StateFactory unitStateFactory,
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
        }

        public override int Key()
        {
            return States.Grounded;
        }

        public override void EnterState()
        {
            InitializeSubState();
        }

        public override void OnUpdateState()
        {
            _container.GroundCheck.Check();
            
            Physics.SphereCast(
                _container.PlayerTransform.position + Vector3.up * 2,
                _container.GroundCheck.CheckRadius / 2,
                Vector3.down,
                out RaycastHit hit, 2.1f, _container.GroundCheck.GroundMask);
            
            _container.YVector = Vector3.down * 0.02f;
            
            var groundY = hit.point.y;
            //var diff = _container.PlayerTransform.position.y - groundY;
            //if (diff > 0.05f || diff < -0.05f)
            {
                Vector3 targetPosition = new Vector3(_container.PlayerTransform.position.x, groundY, _container.PlayerTransform.position.z);

                _container.YVector =
                    (targetPosition - _container.PlayerTransform.position) * (Time.deltaTime * _container.AlignSpeed);
                _container.YVector.z = 0;
                _container.YVector.x = 0;
                _container.CharacterController.Move(_container.YVector);
            }
        }

        public override void ExitState()
        {
            
        }

        public override void CheckSwitchState()
        {
            _container.GroundCheck.Check();
            if (_container.GroundCheck.IsGrounded == false)
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