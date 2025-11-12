using System;
using UnityEngine;

[Serializable]
public class PlayerInfoContainer
{
    public CharacterController CharacterController;
    public Transform CameraTransform;
    public Transform PlayerTransform;
    public GroundCheck GroundCheck;
    public Vector3 MoveVector;
    public float MoveSpeed;
    public float FallSpeed;
    public float AlignSpeed;
    public float RotationSpeed;
    public Vector3 DesiredMoveVector;
    public Vector3 YVector;
}
