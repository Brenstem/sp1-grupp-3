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

    void Start()
    {
        grabbed = false;
        objCurrGrabbed = null;
    }

    void Update()
    {
        GrabCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
            collidingObj = collision.gameObject;
    }

    void GrabCheck()
    {
        if (!grabbed && Input.GetButtonDown("Grab")) {

            grabbed = true;
            Grab();
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
            objCurrGrabbed = collidingObj;
            objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
            Vector2 position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance);
            objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
        }
    }

    void Drop()
    {
        if (collidingObj != null) {
            objCurrGrabbed.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 2f);
            objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = false;
            objCurrGrabbed = null;
        }
    }
}
