using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public bool checkGrounded = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public LayerMask collideWithLayer;

    [SerializeField]
    GameObject dustEffect;

	void Update ()
    {
        var groundHits = new RaycastHit2D[10];
        int hitcount = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundHits);
        checkGrounded = isGrounded;
        isGrounded = hitcount > 0;
        if (checkGrounded == false && isGrounded == true) {
            Instantiate(dustEffect, transform.position + groundCollPosition, transform.rotation);
        }
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);
    }
}