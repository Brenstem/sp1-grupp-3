using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    Transform pointPosition;
    //public Vector2 PointPosition { get { return pointPosition; } }
    public Vector2 boxcastSize;
    [SerializeField]
    float offset;
    [SerializeField]
    float distance;
    [SerializeField]
    float distanceSpeed;
    public Vector2 boxcastOffset;
    //public LayerMask collideWithLayer;

    public bool grabbed;

    GameObject objCurrGrabbed;
    GameObject collidingObj;

    void Start()
    {
        grabbed = false;
        objCurrGrabbed = null;
    }

    void Update()
    {
        //pointPosition = new Vector2(transform.position.x, transform.position.y + offset);
        GrabCheck();
    }

    private void OnDrawGizmos()
    {
        //Gizmos.DrawWireSphere(pointPosition, 0.5f);
        //Gizmos.DrawWireCube((Vector2)transform.position + boxcastOffset, boxcastSize);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
            collidingObj = collision.gameObject;
    }

    void GrabCheck()
    {
        //float inputX = Input.GetAxisRaw("Horizontal");

        //RaycastHit2D detectGrab = Physics2D.BoxCast((Vector2)transform.position + boxcastOffset, boxcastSize, 0, Vector2.zero, 0, collideWithLayer);

        //if (!detectGrab && grabbed == true) {
        //    grabbed = false;
        //    return;
        //}

        if (!grabbed /*&& detectGrab*/ && Input.GetButtonDown("Grab")) {

            grabbed = true;
            Grab();
            //Grab(detectGrab.transform.gameObject);
        }
        else if (grabbed && Input.GetButtonDown("Grab")) {

            grabbed = false;
            Drop();
        }
        else if (!objCurrGrabbed) {
            grabbed = false;
            return;
        }

        if (grabbed) 
        {
            Grab();
        }
    }

    void Grab(/*GameObject obj*/)
    {
        if (collidingObj != null) {
            objCurrGrabbed = collidingObj;
            //objCurrGrabbed.GetComponent<Rigidbody2D>().isKinematic = true;
            //objCurrGrabbed.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Vector2 position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance * distanceSpeed);
            //objCurrGrabbed.transform.position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance * distanceSpeed);
            objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
            objCurrGrabbed.GetComponent<Rigidbody2D>().MoveRotation(0f);
        }
    }

    void Drop()
    {
        if (collidingObj != null) {
            //objCurrGrabbed.GetComponent<Rigidbody2D>().isKinematic = false;
            //objCurrGrabbed.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            objCurrGrabbed.transform.SetParent(null);
            objCurrGrabbed = null;
        }
    }
}
