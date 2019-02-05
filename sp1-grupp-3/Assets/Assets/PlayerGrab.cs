using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    Transform pointPosition;

    [SerializeField]
    float offset;

    [SerializeField]
    float distance;

    public Vector2 boxcastOffset;
    public bool grabbed;

    GameObject objCurrGrabbed;
    GameObject collidingObj;
    DistanceJoint2D joint;
    private Rigidbody2D connectedBody;

    void Start()
    {
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        grabbed = false;
        objCurrGrabbed = null;
    }

    void Update()
    {
        GrabCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>()) {
            collidingObj = collision.gameObject;
        }
    }

    void GrabCheck()
    {
        if (!grabbed && Input.GetButtonDown("Grab")) {
            grabbed = true;
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

    void Grab()
    {
        if (collidingObj != null) {
            joint.enabled = true;
            if (joint)
                joint.connectedBody = connectedBody;
            objCurrGrabbed = collidingObj;
            objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
            Vector2 position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance);
            objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
        }
    }

    void Drop()
    {
        if (collidingObj != null) {
            joint.enabled = false;
            joint.connectedBody = null;
            objCurrGrabbed.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2f);
            objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = false;
            objCurrGrabbed = null;
        }
    }
}
