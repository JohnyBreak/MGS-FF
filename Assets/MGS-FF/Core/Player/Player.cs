using UnitStateMachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private Transform _groundCheckT;
    [SerializeField] private Transform _lookAt;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _groundCheckRadius = 0.35f;
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private float _alignSpeed = 5f;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _rotateSpeed = 3f;
    
    private StateMachine _stateMachine;
    private StateFactory _factory;
    private RaycastHit _hit;
    private GroundCheck _check;
    private PlayerInfoContainer _infoContainer;

    public Transform LookAt => _lookAt;
    
    public void Init(Transform cameraTransform)
    {
        if (!_cameraTransform)
        {
            _cameraTransform = cameraTransform;
        }

        _factory = new StateFactory();
        _stateMachine = new StateMachine();
        _infoContainer = new PlayerInfoContainer();
        _check = new GroundCheck(_groundCheckT, _groundCheckRadius, _groundMask);
        
        _infoContainer.CameraTransform = _cameraTransform;
        _infoContainer.CharacterController = _characterController;
        _infoContainer.PlayerTransform = transform;

        _infoContainer.GroundCheck = _check;
        _infoContainer.FallSpeed = _fallSpeed;
        _infoContainer.AlignSpeed = _alignSpeed;
        
        _infoContainer.RotationSpeed = _rotateSpeed;
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
        
        var vector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        _infoContainer.MoveVector = vector.normalized;
        
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
        
        _stateMachine.Update();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundCheckT.position, _groundCheckRadius);
        if (_check != null && _check.IsGrounded)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(new Vector3(transform.position.x, _hit.point.y, transform.position.z) + Vector3.up * _groundCheckRadius / 2, _groundCheckRadius / 2);
        }
    }
}