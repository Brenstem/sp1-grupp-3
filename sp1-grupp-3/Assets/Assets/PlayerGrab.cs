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
    float inputX = 0;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        grabbed = false;
        objCurrGrabbed = null;
    }

    void Update()
    {
        if(grabbed == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || grabbed == true && isRotating == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            xValue = Input.GetAxisRaw("Horizontal");
        }
        float x = Input.GetAxisRaw("Horizontal");

        //if (x != 0)
        //{
        //    xValue = x;
        //}
        //GrabCheck();

        if(colliding == true && groundCheck.isGrounded)
        {
            if (Input.GetButtonDown("Grab") && grabbed == false && objCurrGrabbed.GetComponent<GroundCheck>().isGrounded == true)
            {
                objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
                objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                objCurrGrabbed.transform.SetParent(parent.transform);
                parent.transform.localPosition = new Vector2(0.3f * -xValue, 1f);

                grabbed = true;
                colliding = false;
            }
        }


        if(grabbed == true)
        {
            if(isRotating == true)
            {
                RotateBox();
            }
            else
            {
                Grab();

                if(Input.GetButtonDown("Grab"))
                {
                    grabbed = false;
                    Drop();
                }
                else if (!objCurrGrabbed)
                {
                    grabbed = false;
                    return;
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            objCurrGrabbed = collision.gameObject;
            colliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            colliding = false;
        }
    }

    void GrabCheck()
    {
        if (colliding == true && groundCheck.isGrounded)
        {
            if (!grabbed && Input.GetButtonDown("Grab"))
            {
                objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
                objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;    

                objCurrGrabbed.transform.SetParent(parent.transform);
                parent.transform.localPosition = new Vector2(0.3f * -xValue, 1f);

                grabbed = true;
                colliding = false;
            }
        }
        else
        {
            if (grabbed && Input.GetButtonDown("Grab") && isRotating == false)
            {
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

        //if (Mathf.Abs(parent.transform.rotation.z) < 0.7f)
        //{
        //    isRotating = true;
            
        //    angleZ = Mathf.MoveTowards(angleZ, 90f * xValue, 2f);
        //    parent.transform.rotation = Quaternion.Euler(0f, 0f, angleZ);

        //    pointPosition.position = objCurrGrabbed.transform.position;
        //    pointPosition.localPosition = new Vector2(0f, pointPosition.localPosition.y);
        //}
        //else
        //{

        //    isRotating = false;
        //}


        //if (isRotating == false)
        //{
            
        //}
        

        Vector2 position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance);
        objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
    }
    void RotateBox()
    {
        if (Mathf.Abs(parent.transform.rotation.z) < 0.7f) // || objCurrGrabbed.transform.rotation.z != 0
        {
            isRotating = true;

            angleZ = Mathf.MoveTowards(angleZ, 90f * xValue, 2f);
            parent.transform.rotation = Quaternion.Euler(0f, 0f, angleZ);

            pointPosition.position = objCurrGrabbed.transform.position;
            pointPosition.localPosition = new Vector2(0f, pointPosition.localPosition.y);

            //float point = objCurrGrabbed.transform.rotation.eulerAngles.z;
            //Debug.Log(point);
            //point = Mathf.MoveTowards(point, 0f, 6f);
            //objCurrGrabbed.transform.rotation = Quaternion.Euler(0f, 0f, point);
        }
        else // if(objCurrGrabbed.transform.rotation.z == 0)
        {
            objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = false;
            objCurrGrabbed.transform.parent = null;
            objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;

            isRotating = false;
        }
    }

    void Drop()
    {

        var force = new Vector2(xForce * xValue, yForce);

        objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = false;
        objCurrGrabbed.GetComponent<Rigidbody2D>().velocity = force;
        objCurrGrabbed = null;

        parent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        angleZ = 0;
        isRotating = true;
    }
    private void OnDrawGizmos()
    {
        if(objCurrGrabbed != null)
        {
            Gizmos.DrawLine(objCurrGrabbed.transform.position, (Vector2)objCurrGrabbed.transform.position + new Vector2(xForce * xValue, yForce));
        }
    }
}
