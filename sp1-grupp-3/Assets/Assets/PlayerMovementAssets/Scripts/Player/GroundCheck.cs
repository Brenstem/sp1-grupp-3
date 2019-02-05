using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public LayerMask collideWithLayer;

	void Update ()
    {
        var groundHits = new RaycastHit2D[10];
        int hitcount = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundHits);
        isGrounded = hitcount > 0;
	}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);
    }
}