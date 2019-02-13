using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckAdvanced : MonoBehaviour
{
    public bool isGrounded = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSizeUD;
    public Vector3 groundCollSizeLR;
    public LayerMask collideWithLayer;

    void Update()
    {
        var groundUpHits = new RaycastHit2D[10];
        int hitUp = Physics2D.BoxCast(transform.position + new Vector3(0f, groundCollPosition.y), groundCollSizeUD, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundUpHits);
        //isGrounded = hitUp > 0;

        var groundDownHits = new RaycastHit2D[10];
        int hitDown = Physics2D.BoxCast(transform.position + new Vector3(0f, -groundCollPosition.y) , groundCollSizeUD, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundDownHits);
        
        var groundRightHits = new RaycastHit2D[10];
        int hitRight = Physics2D.BoxCast(transform.position + new Vector3(groundCollPosition.x, 0f), groundCollSizeLR, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundRightHits);

        var groundLeftHits = new RaycastHit2D[10];
        int hitLeft = Physics2D.BoxCast(transform.position + new Vector3(-groundCollPosition.x, 0f), groundCollSizeLR, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundLeftHits);

        if(hitUp > 0 || hitDown > 0 || hitLeft > 0 || hitRight > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, groundCollPosition.y), groundCollSizeUD);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + new Vector3(0f, -groundCollPosition.y), groundCollSizeUD);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + new Vector3(groundCollPosition.x, 0f), groundCollSizeLR);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(-groundCollPosition.x, 0f), groundCollSizeLR);
    }
}