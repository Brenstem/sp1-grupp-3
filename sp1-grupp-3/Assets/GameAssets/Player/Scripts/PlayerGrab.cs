using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    Transform pointPosition;
    public GameObject parent;

    [SerializeField]
    float distance;
    public float xValue = 0;
    public float yForce;
    public float xForce;
    public float speed;

    //public Vector2 boxcastOffset;
    float myDestination = 0;

    GroundCheck groundCheck;
    GameObject objCurrGrabbed;
    [SerializeField]
    bool colliding = false;
    public bool grabbed;

    public Vector3 boxCollPosition;
    public Vector3 boxCollSize;
    public LayerMask collideWithLayer;
    
    bool isRotating = true;
    bool boxRotated = false;
    float angleZ = 0;
    float inputX = 0;
    Vector2 position = Vector2.zero;
    bool grabMeNow = false;
    bool dropMeNow = false;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        grabbed = false;
        objCurrGrabbed = null;
        xValue = 1;
    }

    void Update()
    {
        if(grabbed == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || grabbed == true && isRotating == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            xValue = Input.GetAxisRaw("Horizontal");
        }
        float x = Input.GetAxisRaw("Horizontal");


        var boxesHit = new RaycastHit2D[10];
        int hitcount = Physics2D.BoxCast(transform.position + boxCollPosition, boxCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithLayer }, boxesHit);

        float pX = boxCollPosition.x * transform.localScale.x;
        float pY = boxCollPosition.y;
        RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + new Vector2(pX,pY), boxCollSize, 0f, Vector2.zero, 0f, collideWithLayer);
        if(hit == true)
        {
            objCurrGrabbed = hit.transform.gameObject;
            colliding = true;
        }
        else
        {
            colliding = false;
        }

        if (colliding == true && groundCheck.isGrounded)
        {
            if (Input.GetButtonDown("Grab") && grabbed == false && objCurrGrabbed.GetComponent<GroundCheck>().isGrounded == true)
            {
                objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
                objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);

                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerJump>().enabled = false;

                objCurrGrabbed.transform.SetParent(parent.transform);

                float xX = Mathf.Abs((objCurrGrabbed.transform.position.x - transform.position.x) / 2);
                parent.transform.localPosition = new Vector2(-xX, 0f);

                grabbed = true;
                colliding = false;
            }
        }


        if(grabbed == true && grabMeNow == true)
        {
            if(isRotating == true)
            {
                RotateBox();
            }
            else
            {
                //Grab();
                if(objCurrGrabbed.GetComponent<BoxCollision>().colliding == true)
                {
                    GetComponent<PlayerMovement>().StopMovement();
                }
                else
                {
                    GetComponent<PlayerMovement>().ContinueMovement();
                }

                if(Input.GetButtonDown("Grab"))
                {
                    grabbed = false;
                }
                else if (!objCurrGrabbed)
                {
                    grabbed = false;
                    return;
                }
            }
        }

        if (grabbed == false && dropMeNow == true)
        {
            Drop();
        }

    }
    
    void GrabCheck()
    {
        if (colliding == true && groundCheck.isGrounded && objCurrGrabbed != null)
        {
            if (!grabbed && Input.GetButtonDown("Grab"))
            {
                objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = true;
                objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;    

                objCurrGrabbed.transform.SetParent(parent.transform);
                parent.transform.localPosition = new Vector2(0.3f * -xValue, 1f);

                //pointPosition.position = new Vector2(objCurrGrabbed.transform.localPosition.x, 0f);

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
            else if (!objCurrGrabbed)
            {
                grabbed = false;
                return;
            }
        }

        //if (grabbed) {
        //    Grab();
        //}
    }

    void Grab()
    {   
        position = Vector2.MoveTowards(objCurrGrabbed.transform.position, pointPosition.position, distance);
        objCurrGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
    }
    float GetRotationDestination(float newPoint)
    {
        float newDestination = 0;

        if (newPoint > 0 && newPoint < 90)
        {
            if (newPoint >= 45)
            {
                newDestination = 90;
            }
            else if (newPoint <= 45)
            {
                newDestination = 0;
            }
        }
        else if (newPoint > 90 && newPoint < 180)
        {
            if (newPoint >= 135)
            {
                newDestination = 180;
            }
            else if (newPoint <= 135)
            {
                newDestination = 90;
            }
        }
        else if (newPoint > 180 && newPoint < 360)
        {
            if (newPoint >= 225)
            {
                newDestination = 360;
            }
            else if (newPoint <= 225)
            {
                newDestination = 180;
            }
        }
        else if (newPoint >= 360)
        {
            newDestination = 0;
        }
        if (newPoint > 0)
        {
            if (newDestination < 0)
            {
                newDestination = -newDestination;
            }
        }
        else
        {
            if (newDestination > 0)
            {
                newDestination = -newDestination;
            }
        }

        return newDestination;
    }

    void RotateBox()
    {
        float point = Mathf.Abs(objCurrGrabbed.transform.rotation.eulerAngles.z);
        myDestination = GetRotationDestination(point);

        if (Mathf.Abs(parent.transform.rotation.z) < 0.7f) // || objCurrGrabbed.transform.rotation.z != 0
        {
            isRotating = true;
            
            //pointPosition.position = objCurrGrabbed.transform.position;
            //pointPosition.localPosition = new Vector2(0f, pointPosition.localPosition.y);
            
            if(boxRotated == false)
            {
                point = Mathf.MoveTowardsAngle(point, myDestination, 10f);
                objCurrGrabbed.transform.rotation = Quaternion.Euler(0f, 0f, point);

                if(point == myDestination)
                {
                    boxRotated = true;
                }
            }
            else
            {
                angleZ = Mathf.MoveTowards(angleZ, 90f * xValue, speed);
                parent.transform.rotation = Quaternion.Euler(0f, 0f, angleZ);

                parent.transform.localPosition = Vector2.MoveTowards(parent.transform.localPosition, new Vector2(parent.transform.localPosition.x, -0.3f), 0.1f);

            }
        }
        else
        {
            pointPosition.position = new Vector2(objCurrGrabbed.transform.position.x, objCurrGrabbed.transform.position.y);
            
            objCurrGrabbed.GetComponent<BoxCollider2D>().isTrigger = false;
            objCurrGrabbed.transform.parent = null;
            objCurrGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            objCurrGrabbed.GetComponent<Rigidbody2D>().gravityScale = 0f;
            objCurrGrabbed.GetComponent<Rigidbody2D>().mass = 0.001f;


            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerJump>().enabled = true;
            //pointPosition.position = new Vector2(pointPosition.position.x, pointPosition.position.y + 0.10f);
            //    pointPosition.localPosition = new Vector2(pointPosition.localPosition.x, pointPosition.position.y);
            isRotating = false;
        }
    }

    void FixedUpdate()
    {
        if (grabbed == true && grabMeNow == true)
        {
            if(isRotating == false)
            {
                Grab();
            }
        }
    }

    void Drop()
    {
        var force = new Vector2(xForce * xValue, yForce);

        objCurrGrabbed.GetComponent<Rigidbody2D>().gravityScale = 1f;
        objCurrGrabbed.GetComponent<Rigidbody2D>().mass = 1f;
        objCurrGrabbed.GetComponent<Rigidbody2D>().freezeRotation = false;
        objCurrGrabbed.GetComponent<Rigidbody2D>().velocity = force;
        objCurrGrabbed = null;

        parent.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        parent.transform.localPosition = Vector2.zero;
        //pointPosition.position = new Vector2(objCurrGrabbed.transform.localPosition.x, objCurrGrabbed.transform.localPosition.y);
        angleZ = 0;
        isRotating = true;
        grabMeNow = false;
        boxRotated = false;
        dropMeNow = false;
    }

    void OnDrawGizmos()
    {
        if(objCurrGrabbed != null)
        {
            Gizmos.DrawLine(objCurrGrabbed.transform.position, (Vector2)objCurrGrabbed.transform.position + new Vector2(xForce * xValue, yForce));
        }
        float pX = boxCollPosition.x * transform.localScale.x;
        float pY = boxCollPosition.y;
        Gizmos.DrawWireCube((Vector2)transform.position + new Vector2(pX, pY), boxCollSize);
    }

    Vector2 GetBoxSize()
    {
        return objCurrGrabbed.GetComponent<BoxCollider2D>().size;
    }
    Vector2 Distance()
    {
        return new Vector2(objCurrGrabbed.transform.position.x - transform.position.x, objCurrGrabbed.transform.position.y - transform.position.y);
    }

    public void GrabMePlease()
    {
        grabMeNow = true;
    }
    public void DropMePlease()
    {
        dropMeNow = true;
    }
}