using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveObject : PhysicsObject
{
    void Start()
    {
        
    }
    
    void Update()
    {
        targetVelocity = Vector2.left;
    }
}
