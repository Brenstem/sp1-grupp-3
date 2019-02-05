using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public MovementState movementState;
    public MovementState defaultMovementState;
    [Header("MovementSettings Only For Show")]
    public MovementSettings movementSettings;
    Rigidbody2D rb;
    float velX = 0;

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
	}
    
	void Update ()
    {
        if(movementState != null)
        {
            UpdateMovementState(movementState);
        }
        else
        {
            UpdateMovementState(defaultMovementState);
        }

        InputHorizontal();
    }

    public void UpdateMovementState(MovementState move)
    {
        movementSettings.deAcceleration = move.deAcceleration;
        movementSettings.acceleration = move.acceleration;
        movementSettings.speed = move.speed;
        //movementSettings.maxSpeed = move.maxSpeed;
        //movementSettings.grabMoveAcceleration = move.grabMoveAcceleration;
    }

    void InputHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float ctrlHorizontal = Input.GetAxisRaw("CtrlHorizontal");

        if(Mathf.Abs(horizontal) > 0)
        {
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

        //velX = Mathf.Clamp(velX, -movementSettings.maxSpeed, movementSettings.maxSpeed);
        Vector2 velocity = new Vector2(velX, rb.velocity.y);

        rb.velocity = velocity;
    }
}