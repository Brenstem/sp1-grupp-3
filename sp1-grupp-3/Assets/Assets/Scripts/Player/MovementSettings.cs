using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovementSettings
{
    public string name;

    public float deAcceleration;
    public float acceleration;

    public float speed;
    public float maxSpeed;

    public float grabMoveAcceleration;
}