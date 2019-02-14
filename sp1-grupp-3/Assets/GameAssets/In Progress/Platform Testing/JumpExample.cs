using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementPlatform))]
public class JumpExample : MonoBehaviour
{
    Rigidbody2D rb;
    MovementPlatform movementPlatformTest;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementPlatformTest = GetComponent<MovementPlatform>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) {
            Vector2 platformVelocity = movementPlatformTest.GetPlatformVelocity();
           
            rb.velocity = Vector2.up * 5 + platformVelocity;
        }
    }
}
