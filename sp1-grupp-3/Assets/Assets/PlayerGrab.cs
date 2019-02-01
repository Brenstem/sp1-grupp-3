using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{

    private Vector2 pointPosition;
    public Vector2 PointPosition { get { return pointPosition; } }
    public Vector2 boxcastSize;
    public float offset;
    public Vector2 boxcastOffset;
    public LayerMask collideWithLayer;

    public bool grabbed;
    GameObject carriedObj;

    void Start()
    {
        grabbed = false;
    }

    void Update()
    {
        pointPosition = new Vector2(transform.position.x, transform.position.y + offset);
        GrabCheck();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointPosition, 0.5f);
        Gizmos.DrawWireCube((Vector2)transform.position + boxcastOffset, boxcastSize);
    }

    void GrabCheck()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        
        RaycastHit2D detectGrab = Physics2D.BoxCast((Vector2)transform.position + boxcastOffset, boxcastSize, 0, Vector2.zero, 0, collideWithLayer);

        if (!grabbed && detectGrab && Input.GetButtonDown("Grab")){
            grabbed = true;
            Grab(gameObject);
        }
        else if(grabbed && Input.GetButtonDown("Grab")) {
            grabbed = false;
        }
    }

    void Grab(GameObject obj)
    {
        obj.transform.position = Vector2.MoveTowards(obj.transform.position, pointPosition, 0);
    }
}
