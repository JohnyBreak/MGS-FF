using UnityEngine;

public class GroundCheck
{
    public Transform GroundCheckTransform { get; }
    public float CheckRadius { get; }
    public LayerMask GroundMask { get; }
    
    public bool IsGrounded;// => Physics.CheckSphere(GroundCheckTransform.position, CheckRadius, GroundMask);
    
    public GroundCheck(Transform checkT, float radius, LayerMask groundMask)
    {
        GroundCheckTransform = checkT;
        CheckRadius = radius;
        GroundMask = groundMask;
    }

    public void Check()
    {
        IsGrounded = Physics.CheckSphere(GroundCheckTransform.position, CheckRadius, GroundMask);
    }
}
