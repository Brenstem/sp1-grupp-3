using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isWalled = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public Vector3 wallCollPosition;
    public Vector3 wallCollSize;
    public LayerMask collideWithLayer;
    PlayerMovement movement;
    public float slopeFriction;

    void Start()
    {
        if(GetComponent<PlayerMovement>() != null)
        {
            movement = GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        var groundHits = new RaycastHit2D[10];
        int hitcount = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundHits);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, 0f, collideWithLayer);
        isGrounded = hitcount > 0;
        
        if(isGrounded == true)
        {
            Vector2 position = transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(rb.velocity.x - (hit.normal.x * slopeFriction), rb.velocity.y);
        }
        
        if(movement != null)
        {
            var wallHits = new RaycastHit2D[10];
            int wallHitCount = (Physics2D.BoxCast(transform.position + (wallCollPosition * transform.localScale.x), wallCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, wallHits));
            isWalled = wallHitCount > 0;

            if (isWalled == true)
            {
                movement.StopMovement();
            }
            else
            {
                movement.ContinueMovement();
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);
        Gizmos.DrawWireCube(transform.position + (wallCollPosition * transform.localScale.x), wallCollSize);
    }
}