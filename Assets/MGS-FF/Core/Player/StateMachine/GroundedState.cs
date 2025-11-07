using UnityEngine;

namespace UnitStateMachine
{
    public class GroundedState : BaseState
    {
        private readonly PlayerInfoContainer _container;

        public GroundedState(
            StateMachine currentContext, 
            StateFactory unitStateFactory,
            PlayerInfoContainer container)
            : base(currentContext, unitStateFactory)
        {
            _container = container;
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
            Physics.SphereCast(
                _container.PlayerTransform.position + Vector3.up * 2,
                _container.GroundCheck.CheckRadius / 2,
                Vector3.down,
                out RaycastHit hit, 2.1f, _container.GroundCheck.GroundMask);
                
            var groundY = hit.point.y;
            var diff = _container.PlayerTransform.position.y - groundY;
            if (diff > 0.05f || diff < -0.05f)
            {
                Vector3 targetPosition = new Vector3(_container.PlayerTransform.position.x, groundY, _container.PlayerTransform.position.z);
                _container.PlayerTransform.position = Vector3.Lerp(_container.PlayerTransform.position, targetPosition, Time.deltaTime * _container.AlignSpeed);
            }
        }

        public override void ExitState()
        {
            Debug.Log("Exit GroundedState");
        }

        public override void CheckSwitchState()
        {
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