using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatformTest : MonoBehaviour
{
    Rigidbody2D rb;
    Collision2D collisionPlatform;
    Platform platform;
    Vector2 platformVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 0.0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Platfrom")) {
        platform = collision.gameObject.GetComponent<Platform>();
        transform.parent = platform.transform;
        //}
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag("Platfrom")) {
        platform = null;
        transform.parent = null;
        //}
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (platform != null) {
                platformVelocity = platform.velocity;
            }
            else {
                platformVelocity = Vector2.zero;

            }
            rb.velocity = Vector2.up * 5 + platformVelocity;
            platformVelocity = Vector2.zero;
        }
    }
}
