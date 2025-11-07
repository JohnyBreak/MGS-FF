using UnitStateMachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckRadius = 0.35f;
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private float _alignSpeed = 5f;
    [SerializeField] private float _moveSpeed = 3f;

    private StateMachine _stateMachine;
    private StateFactory _factory;
    private RaycastHit _hit;
    private GroundCheck _check;
    private PlayerInfoContainer _infoContainer;

    private void Start()
    {
        _factory = new StateFactory();
        _stateMachine = new StateMachine();
        _infoContainer = new PlayerInfoContainer();
        _infoContainer.PlayerTransform = transform;

        _check = new GroundCheck(transform, _groundCheckRadius, _groundMask);
        _infoContainer.GroundCheck = _check;
        _infoContainer.FallSpeed = _fallSpeed;
        _infoContainer.AlignSpeed = _alignSpeed;
        _infoContainer.MoveSpeed = _moveSpeed;

        var grounded = new GroundedState(_stateMachine, _factory, _infoContainer);
        var air = new AirState(_stateMachine, _factory, _infoContainer);
        var idle = new IdleState(_stateMachine, _factory, _infoContainer);
        var move = new MoveState(_stateMachine, _factory, _infoContainer);

        _factory.AddState(grounded);
        _factory.AddState(air);
        _factory.AddState(idle);
        _factory.AddState(move);

        _stateMachine.SetState(_factory.Get(States.Grounded));
        _stateMachine.Start();
    }

    private void Update()
    {
        var speedMultiplier = (Input.GetKey(KeyCode.LeftShift)) ? 0.5f : 1f;
        _infoContainer.MoveSpeed = _moveSpeed * speedMultiplier;
        var vector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _infoContainer.MoveVector = vector.normalized;

        _stateMachine.Update();

        if (_check != null &&
            _check.IsGrounded &&
            Physics.SphereCast(
                transform.position + Vector3.up * 2,
                _groundCheckRadius / 2,
                Vector3.down,
                out RaycastHit hit, 2.1f, _groundMask))
        {
            _hit = hit;
        }

        // if (_check != null &&
        //     _check.IsGrounded)
        // {
        //     var groundY = _hit.point.y;
        //     var diff = transform.position.y - groundY;
        //     if (diff > 0.05f || diff < -0.05f)
        //     {
        //         Vector3 targetPosition = new Vector3(transform.position.x, groundY, transform.position.z);
        //         //transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _alignSpeed);
        //     }
        // }
        // else
        // {
        //     //transform.Translate(Vector3.down * (_fallSpeed * Time.deltaTime));
        // }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _groundCheckRadius);
        if (_check != null && _check.IsGrounded)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(_hit.point + (Vector3.up * _groundCheckRadius / 2), _groundCheckRadius / 2);
        }
    }
}