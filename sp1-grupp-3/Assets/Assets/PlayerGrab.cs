using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    Transform pointPosition;

    [SerializeField]
    float distance;

    public Vector2 boxcastOffset;
    public bool grabbed;

    GroundCheck groundCheck;
    GameObject objCurrGrabbed;
    //DistanceJoint2D joint;
    //private Rigidbody2D connectedBody;
    bool colliding = false;
    public GameObject parent;
    public float xValue = 0;
    public float yForce;
    public float xForce;
    bool isRotating = true;
    float angleZ = 0;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        //joint = GetComponent<DistanceJoint2D>();
        //joint.enabled = false;
        grabbed = false;
        objCurrGrabbed = null;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        if (x != 0) {
            xValue = x;
        }
        GrabCheck();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>()) {
            objCurrGrabbed = collision.gameObject;

            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>()) {
            colliding = false;
        }
    }

    void GrabCheck()
    {
        if (colliding == true && groundCheck.isGrounded) {
            if (!grabbed && Input.GetButtonDown("Grab")) {
                objCurrGrabbed.transform.SetParent(parent.transform);
                //joint.enabled = true;
                //joint.connectedBody = objCurrGrabbed.GetComponent<Rigidbody2D>();
                objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
                objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                parent.transform.localPosition = new Vector2(0.3f * -xValue, 1f);

                grabbed = true;
                colliding = false;
            }
        }
        else {
            if (grabbed && Input.GetButtonDown("Grab") && isRotating == false) {
                grabbed = false;
                Drop();
            }
            else if (!objCurrGrabbed) {
                grabbed = false;
                return;
            }
        }

        if (grabbed) {
            Grab();
        }
    }

    void Grab()
    {
        // objCurrGrabbed = collidingObj;
        //Debug.Log(parent.transform.rotation.z);

        if (Mathf.Abs(parent.transform.rotation.z) < 0.7f) {
            isRotating = true;
            
            angleZ = Mathf.MoveTowards(angleZ, 90f * xValue, 2f);
            parent.transform.rotation = Quaternion.Euler(0f, 0f, angleZ);

            pointPosition.position = objCurrGrabbed.transform.position;
            pointPosition.localPosition = new Vector2(0f, pointPosition.localPosition.y);
        }
        else {

            isRotating = false;
        }


        if (isRotating == false) {
            objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = false;
            objCurrGrabbed.transform.parent = null;
            objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            Vector2 position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance);
            objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
        }
    }

    void Drop()
    {
        var force = new Vector2(xForce * xValue, yForce);

        objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = false;
        //pointPosition = null;
        parent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        angleZ = 0;
        //joint.enabled = false;
        //joint.connectedBody = null;
        objCurrGrabbed.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);

        objCurrGrabbed = null;
    }
}
