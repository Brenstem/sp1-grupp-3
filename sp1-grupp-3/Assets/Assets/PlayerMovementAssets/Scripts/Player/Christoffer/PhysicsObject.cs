﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float minGroundNormalY = 0.65f;
    public float gravityModifier = 1f;

    protected Vector2 targetVelocity;
    protected bool grounded;
    protected Vector2 groundNormal;
    protected Rigidbody2D rb;
    protected Vector2 velocity;
    protected ContactFilter2D contactFiler;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[9];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(1);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFiler.useTriggers = false;
        contactFiler.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFiler.useLayerMask = true;
    }
    
    void Update()
    {
        //targetVelocity = new Vector2(0, 0);
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    private void FixedUpdate()
    {
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 moveAlongGround = new Vector2(groundNormal.y, groundNormal.x);
        Vector2 deltaPos = velocity * Time.deltaTime;
        Vector2 move = moveAlongGround * deltaPos.x;
        
        Movement(move, false);
        move = Vector2.up * deltaPos.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if(distance > minMoveDistance) 
        {
            int count = rb.Cast(move, contactFiler, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();

            for (int i = 0; i < count; i++) {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++) {
                Vector2 currentNormal = hitBufferList[i].normal;
                
                if(currentNormal.y > minGroundNormalY) {
                    grounded = true;
                    if (yMovement) {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }
                float projection = Vector2.Dot(velocity, currentNormal);

                if(projection < 0) {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }
        rb.position = rb.position + move.normalized * distance;
    }
}
