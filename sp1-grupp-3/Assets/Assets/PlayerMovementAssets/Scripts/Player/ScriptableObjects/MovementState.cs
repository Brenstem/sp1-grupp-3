using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementState : ScriptableObject
{
    [Header("Movement")]
    public float speed;
    public float acceleration;
    public float deAcceleration;
    //public float maxSpeed;
    //[Space]
    //public float grabMoveAcceleration;

    [Space]
    [Header("Jump")]
    public float jumpStrength;
    public float maxJumpHeight;
    public float maxJumpSustain;
    public float jumpGravity; 
    public float fallGravity;
}