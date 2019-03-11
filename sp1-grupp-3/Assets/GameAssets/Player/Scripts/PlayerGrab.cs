using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerGrab : MonoBehaviour
{
    private Transform pointPosition;
    private Transform parentPosition;

    [SerializeField]
    float followSpeed;

    public float throwForceX;
    public float throwForceY;
    public float rotationSpeed;

    [SerializeField]
    bool colliding = false;
    public bool grabbed;

    public Vector3 boxCollPosition;
    public Vector3 boxCollSize;
    public LayerMask collideWithLayer;

    GameObject objGrabbed;
    Vector2 position = Vector2.zero;
    int grabbedDirection = 0;
    float angleZ = 0;
    float myDestination = 0;
    bool isRotating = true;
    bool boxRotated = false;
    bool grabMeNow = false;
    bool dropMeNow = false;
    CapsuleCollider2D currentCollider;

    public Vector2 capsuleOffset;
    public Vector2 capsuleSize;
    public Vector2 capsuleOffsetBox;
    public Vector2 capsuleSizeBox;

    GroundCheck groundCheck;
    Rigidbody2D rb;

    void Start()
    {
        groundCheck = GetComponent<GroundCheck>();
        rb = GetComponent<Rigidbody2D>();
        currentCollider = GetComponent<CapsuleCollider2D>();
        pointPosition = transform.Find("Point Position");
        parentPosition = transform.Find("Parent Position");
        grabbed = false;
        objGrabbed = null;
        grabbedDirection = 1;
    }

    void Update()
    {
        if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
        {
            if (grabbed == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || grabbed == true && isRotating == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0)
            {
                grabbedDirection = (int)Input.GetAxisRaw("Horizontal");
            }
        }
        else if(Mathf.Abs(Input.GetAxisRaw("CtrlHorizontal")) > 0)
        {
            if (grabbed == false && Mathf.Abs(Input.GetAxisRaw("CtrlHorizontal")) > 0 || grabbed == true && isRotating == false && Mathf.Abs(Input.GetAxisRaw("CtrlHorizontal")) > 0)
            {
                if(Input.GetAxisRaw("CtrlHorizontal") > 0.5f)
                {
                    grabbedDirection = 1;
                }
                else if(Input.GetAxisRaw("CtrlHorizontal") < -0.5f)
                {
                    grabbedDirection = -1;
                }
            }
        }
        
        float pX = transform.position.x + boxCollPosition.x * transform.localScale.x;
        float pY = transform.position.y + boxCollPosition.y;
        RaycastHit2D hit = Physics2D.BoxCast(new Vector2(pX, pY), boxCollSize, 0f, Vector2.zero, 0f, collideWithLayer);

        if (hit == true)
        {
            if (grabbed == false && hit.transform.GetComponent<GroundCheck>().isGrounded == true)
            {
                objGrabbed = hit.transform.gameObject;
                colliding = true;
            }
        }
        else
        {
            colliding = false;
        }

        if (colliding == true && groundCheck.isGrounded)
        {
            if (Input.GetButtonDown("Grab") && grabbed == false) //&& objGrabbed.GetComponent<GroundCheck>().isGrounded == true
            {
                Rigidbody2D objRb = objGrabbed.GetComponent<Rigidbody2D>();

                objRb.freezeRotation = true;
                objRb.bodyType = RigidbodyType2D.Kinematic;
                rb.velocity = new Vector2(0f, rb.velocity.y);
                objGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
                currentCollider.offset = capsuleOffsetBox;
                currentCollider.size = capsuleSizeBox;

                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerJump>().enabled = false;

                objGrabbed.transform.SetParent(parentPosition.transform);

                float xX = Mathf.Abs((transform.position.x - objGrabbed.transform.position.x) / 2);
                parentPosition.transform.localPosition = new Vector2(-xX, 0f);

                grabbed = true;
                colliding = false;
            }
        }

        if (grabbed == true && grabMeNow == true)
        {
            if (isRotating == true)
            {
                RotateBox();
            }
            else
            {
                if (Input.GetButtonDown("Grab"))
                {
                    grabbed = false;
                }
                else if (!objGrabbed)
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

    void FixedUpdate()
    {
        if (grabbed == true && grabMeNow == true)
        {
            if (isRotating == false)
            {
                MoveBoxToPosition();
            }
        }
    }

    void MoveBoxToPosition()
    {
        //position = Vector2.MoveTowards(objGrabbed.transform.position, pointPosition.position, followSpeed);
        //objGrabbed.GetComponent<Rigidbody2D>().MovePosition(position);
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
        float point = Mathf.Abs(objGrabbed.transform.rotation.eulerAngles.z);
        myDestination = GetRotationDestination(point);

        if (Mathf.Abs(parentPosition.transform.rotation.z) < 0.7f) // || objCurrGrabbed.transform.rotation.z != 0
        {
            isRotating = true;

            if (boxRotated == false)
            {
                point = Mathf.MoveTowardsAngle(point, myDestination, 10f);
                objGrabbed.transform.rotation = Quaternion.Euler(0f, 0f, point);

                if (point == myDestination)
                {
                    boxRotated = true;
                }
            }
            else
            {
                angleZ = Mathf.MoveTowards(angleZ, 90f * grabbedDirection, rotationSpeed);
                parentPosition.transform.rotation = Quaternion.Euler(0f, 0f, angleZ);
            }
        }
        else
        {
            pointPosition.transform.position = objGrabbed.transform.position;

            objGrabbed.GetComponent<BoxCollider2D>().isTrigger = true;
            //objGrabbed.transform.parent = null;
            objGrabbed.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //objGrabbed.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            //objGrabbed.GetComponent<Rigidbody2D>().mass = 0.001f;

            GetComponent<PlayerMovement>().enabled = true;
            GetComponent<PlayerJump>().enabled = true;
            isRotating = false;
        }
    }

    void Drop()
    {
        var force = new Vector2(throwForceX * grabbedDirection, throwForceY);

        Rigidbody2D objRB = objGrabbed.GetComponent<Rigidbody2D>();
        objRB.transform.parent = null;
        objRB.constraints = RigidbodyConstraints2D.None;
        objRB.constraints = RigidbodyConstraints2D.FreezeRotation;
        objRB.bodyType = RigidbodyType2D.Dynamic;
        objGrabbed.GetComponent<BoxCollider2D>().isTrigger = false;
        currentCollider.offset = capsuleOffset;
        currentCollider.size = capsuleSize;

        objRB.gravityScale = 1f;
        objRB.mass = 1f;
        objRB.freezeRotation = false;
        objRB.velocity = force;
        objGrabbed = null;

        parentPosition.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        parentPosition.transform.localPosition = Vector2.zero;

        angleZ = 0;
        isRotating = true;
        grabMeNow = false;
        boxRotated = false;
        dropMeNow = false;
    }

    void OnDrawGizmos()
    {
        if (objGrabbed != null)
        {
            Gizmos.DrawLine(objGrabbed.transform.position, (Vector2)objGrabbed.transform.position + new Vector2(throwForceX * grabbedDirection, throwForceY));
        }
        float pX = transform.position.x + boxCollPosition.x * transform.localScale.x;
        float pY = transform.position.y + boxCollPosition.y;
        Gizmos.DrawWireCube(new Vector2(pX, pY), boxCollSize);
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