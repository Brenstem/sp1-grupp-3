using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public LayerMask collideWithLayer;
    bool previousGrounded = false;

	void Update ()
    {
        var groundHits = new RaycastHit2D[10];
        int hitcount = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, groundHits);
        previousGrounded = isGrounded;
        isGrounded = hitcount > 0;

        if(transform.tag != "Player")
        {
            Debug.Log(GetComponent<Rigidbody2D>().velocity);
        }
        //if (previousGrounded == false && isGrounded == true)
        //{
        //    Debug.Log("Landed");
        //}
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);
    }
}