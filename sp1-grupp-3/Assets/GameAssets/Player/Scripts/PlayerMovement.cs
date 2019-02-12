using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementState newMovementState;
    public MovementState defaultMovementState;

    float speed;
    float acceleration;
    float deAcceleration;

    Rigidbody2D rb;
    float velX = 0;
    public bool enableNewMovement = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (newMovementState != null && enableNewMovement == true)
        {
            UpdateMovementState(newMovementState);
        }
        else if (newMovementState == null || newMovementState != null && enableNewMovement == false)
        {
            UpdateMovementState(defaultMovementState);
        }

        InputHorizontal();
    }

    public void UpdateMovementState(MovementState move)
    {
        speed = move.speed;
        acceleration = move.acceleration;
        deAcceleration = move.deAcceleration;
    }

    void InputHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); //Get Input For Keyboard
        float ctrlHorizontal = Input.GetAxisRaw("CtrlHorizontal"); //Get Input For Controller

        if (Mathf.Abs(horizontal) > 0) //Check If The Keyboard Is Pressed Down
        {
            int direction = 0; //Make A Variable That Will Store The Direction Of The Player, Depending On Where They Press
            if (horizontal > 0) //If The Player Pressed "Right"
            {
                direction = 1; //Direction Is 1 = Right
            }
            else if (horizontal < 0) //If The Player Pressed "Left"
            {
                direction = -1; //Direction Is -1 = Left
            }
            velX = Mathf.MoveTowards(velX, speed * direction, acceleration * Time.deltaTime); //Here I Set So That velX Accelerates Towards Speed. (speed * direction) To Get The Correct Velocity
        }
        else if (ctrlHorizontal == 0) //If The Controller Isn't Moving & Player Is Not Pressing The Keyboard, Deaccelerate.
        {
            velX = Mathf.MoveTowards(velX, 0f, deAcceleration * Time.deltaTime); //velx DeAccelerates Towards Zero.
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
            velX = Mathf.MoveTowards(velX, speed * direction, acceleration * Time.deltaTime);
        }
        else if (horizontal == 0)
        {
            velX = Mathf.MoveTowards(velX, 0f, deAcceleration * Time.deltaTime);
        }
        
        Vector2 velocity = new Vector2(velX, rb.velocity.y);

        rb.velocity = velocity;
    }
}