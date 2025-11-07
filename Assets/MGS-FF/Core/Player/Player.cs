using UnitStateMachine;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMachine _stateMachine;
    private StateFactory _factory;
    
    private void Start()
    {
        _factory = new StateFactory();
        _stateMachine = new StateMachine();
        var grounded = new GroundedState(_stateMachine, _factory);
        var air = new AirState(_stateMachine, _factory);
        var idle = new IdleState(_stateMachine, _factory);
        var move = new MoveState(_stateMachine, _factory);
        
        _factory.AddState(grounded);
        _factory.AddState(air);
        _factory.AddState(idle);
        _factory.AddState(move);
        
        _stateMachine.SetState(_factory.Get(States.Grounded));
        _stateMachine.Start();
    }

    private void Update()
    {
        _stateMachine.Update();
    }
}
