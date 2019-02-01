using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementState : ScriptableObject
{
    [Header("Movement")]
    public float deAcceleration;
    public float acceleration;
    public float speed;
    public float maxSpeed;

    [Space]
    [Header("Jump")]
    public float jumpStrength;
    public float maxJumpHeight;
    public float jumpGravity; 
    public float fallGravity;
}