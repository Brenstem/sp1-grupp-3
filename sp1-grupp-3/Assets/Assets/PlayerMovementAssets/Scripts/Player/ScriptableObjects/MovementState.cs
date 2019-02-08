using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MovementState : ScriptableObject
{
    [Header("Movement")]
    public float maxSpeed;
    public float acceleration;
    public float deAcceleration;

    [Space]
    [Header("Jump")]
    public float jumpForce;
    public float gravityModifier; 
}