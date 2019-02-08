using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementState newMovementState;
    public MovementState defaultMovementState;
<<<<<<< HEAD:sp1-grupp-3/Assets/Assets/PlayerMovementAssets/Scripts/Player/PlayerMovement.cs
    [Space]
    [Header("MovementSettings Only For Show")]
=======
>>>>>>> parent of a268741... Player Movement Updated:sp1-grupp-3/Assets/Assets/Scripts/Player/PlayerMovement.cs
    public MovementSettings movementSettings;
    Rigidbody2D rb;
    float Speed = 0;

    bool movingLeft = false;
    bool movingRight = false;
    public float moveSpeed = 1f;
    float value = 0;
    float inputAxis = 0;
    float velX = 0;
    public bool enableNewMovement = false;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
       
	}
    
	void Update ()
    {
        if(newMovementState != null && enableNewMovement == true)
        {
            UpdateMovementState(newMovementState);
        }
        else if(newMovementState == null || newMovementState != null && enableNewMovement == false)
        {
            UpdateMovementState(defaultMovementState);
        }
        

        //float horizontal = Input.GetAxis("Horizontal");
        //float ctrlHorizontal = Input.GetAxis("CtrlHorizontal");
        //Debug.Log(ctrlHorizontal);

        //float input = 0;
        //if (horizontal == 0)
        //{
        //    //if(Mathf.Abs(ctrlHorizontal) > 0.1f && Mathf.Abs(ctrlHorizontal) < 0.6f)
        //    //{
        //    //    ctrlHorizontal = 0;
        //    //}

        //    input = ctrlHorizontal;
        //    // Debug.Log(ctrlHorizontal);
        //}
        //else if (ctrlHorizontal == 0)
        //{
        //    input = horizontal;
        //}

        //input *= movementSettings.speed;
        //input = Mathf.Clamp(input, -movementSettings.maxSpeed, movementSettings.maxSpeed);
        InputHorizontal();
        //rb.velocity = new Vector2(input, rb.velocity.y);
    }

    void UpdateMovementState(MovementState move)
    {
        movementSettings.speed = move.speed;
<<<<<<< HEAD:sp1-grupp-3/Assets/Assets/PlayerMovementAssets/Scripts/Player/PlayerMovement.cs
        movementSettings.acceleration = move.acceleration;
        movementSettings.deAcceleration = move.deAcceleration;
        //movementSettings.maxSpeed = move.maxSpeed;
        //movementSettings.grabMoveAcceleration = move.grabMoveAcceleration;
=======
        movementSettings.maxSpeed = move.maxSpeed;
        movementSettings.grabMoveAcceleration = move.grabMoveAcceleration;
>>>>>>> parent of a268741... Player Movement Updated:sp1-grupp-3/Assets/Assets/Scripts/Player/PlayerMovement.cs
    }

    void InputHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float ctrlHorizontal = Input.GetAxisRaw("CtrlHorizontal");

        //if (ctrlHorizontal == 0)
        //{
        //    velX = horizontal;
        //    velX *= movementSettings.speed;
        //}
        //else if (horizontal == 0)
        //{

        //}
        if(Mathf.Abs(horizontal) > 0)
        {
            //velX *= movementSettings.speed;
            int direction = 0;
            if (horizontal > 0)
            {
                direction = 1;
            }
            else if (horizontal < 0)
            {
                direction = -1;
            }

            velX = Mathf.MoveTowards(velX, movementSettings.speed * direction, movementSettings.acceleration * Time.deltaTime);
        }
        else
        {
            velX = Mathf.MoveTowards(velX, 0f, movementSettings.deAcceleration * Time.deltaTime);
        }

        if (Mathf.Abs(ctrlHorizontal) > 0 && Mathf.Abs(horizontal) <= 0)
        {
            int direction = 0;
            if (ctrlHorizontal > 0)
            {
                direction = 1;
            }
            else if (ctrlHorizontal < 0)
            {
                direction = -1;
            }
            velX = Mathf.MoveTowards(velX, movementSettings.speed * direction, movementSettings.acceleration * Time.deltaTime);
        }
        else
        {
            velX = Mathf.MoveTowards(velX, 0f, movementSettings.deAcceleration * Time.deltaTime);
        }

        velX = Mathf.Clamp(velX, -movementSettings.maxSpeed, movementSettings.maxSpeed);
        Vector2 velocity = new Vector2(velX, rb.velocity.y);

        rb.velocity = velocity;
    }

    void Whatever()
    {
        if(UnityEngine.Input.GetAxis("CtrlHorizontal") < 0 && (Speed < movementSettings.maxSpeed))
        {
            Speed = Speed - movementSettings.acceleration * Time.deltaTime;
            Speed *= movementSettings.speed;
        }
        else if(UnityEngine.Input.GetAxis("CtrlHorizontal") > 0 && (Speed > -movementSettings.maxSpeed))
        {
            Speed = Speed + movementSettings.acceleration * Time.deltaTime;
            Speed *= movementSettings.speed;
        }
        else
        {
            if(Speed > movementSettings.deAcceleration)
            {
                Speed = Speed - movementSettings.deAcceleration * Time.deltaTime;
                Speed *= movementSettings.speed;
            }
            else if(Speed < -movementSettings.deAcceleration)
            {
                Speed = Speed + movementSettings.deAcceleration * Time.deltaTime;
                Speed *= movementSettings.speed;
                
            }
            else
            {
                Speed = 0;
            }
        }
        Speed = Mathf.Clamp(Speed, -movementSettings.maxSpeed, movementSettings.maxSpeed);
        rb.velocity = new Vector2(Speed, rb.velocity.y);
    }
}