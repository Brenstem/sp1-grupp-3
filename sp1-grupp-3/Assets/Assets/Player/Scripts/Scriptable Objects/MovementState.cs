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

    [Space]
    [Header("Jump")]
    public float jumpHeight;
    public float fallGravity;

}
