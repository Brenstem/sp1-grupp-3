﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    Rigidbody2D rb;
    Collision2D collisionPlatform;
    Platform platform;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 collisionNormal = collision.GetContacts.

        if (collision.gameObject.CompareTag("PlatformMoving")) {


            platform = collision.gameObject.GetComponent<Platform>();
            transform.parent = platform.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlatformMoving")) {
            platform = null;
            transform.parent = null;
        }
    }

    public Vector2 GetPlatformVelocity()
    {
        Vector2 platformVelocity;

        if (platform != null) {
            platformVelocity = platform.velocity;
        }
        else {
            platformVelocity = Vector2.zero;

        }

        return platformVelocity;
    }

}
