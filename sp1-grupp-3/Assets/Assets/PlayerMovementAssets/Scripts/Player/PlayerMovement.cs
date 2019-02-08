using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsObject
{
    [Space]
    public MovementState newMovementState;
    public MovementState defaultMovementState;
    //public MovementSettings movementSettings;
    [Space]
    public float maxSpeed = 7;
    public float jumpForce = 7;
    public float acceleration;
    public float deAcceleration;
    float velX = 0;

    public bool enableNewMovement = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    //private float currentSpeed;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    public void UpdateMovementState(MovementState move)
    {
        acceleration = move.acceleration;
        deAcceleration = move.deAcceleration;
    }

    protected override void ComputeVelocity()
    {
        if (newMovementState != null && enableNewMovement == true)
        {
            UpdateMovementState(newMovementState);
        }
        else if (newMovementState == null || newMovementState != null && enableNewMovement == false)
        {
            UpdateMovementState(defaultMovementState);
        }

        Vector2 move = Vector2.zero;
        move = InputHorizontal();

        Jump();

        FlipSprite(move);
        AnimationParameters();

        targetVelocity = move * maxSpeed;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpForce;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }
    }

    private void AnimationParameters()
    {
        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x / maxSpeed));
    }

    private void FlipSprite(Vector2 move)
    {
        bool flipSprite = spriteRenderer.flipX ? (move.x < -0.01f) : (move.x > 0.01f);

        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    Vector2 InputHorizontal()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float ctrlHorizontal = Input.GetAxisRaw("CtrlHorizontal");

        if (Mathf.Abs(horizontal) > 0)
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

            velX = Mathf.MoveTowards(velX, maxSpeed * direction, acceleration * Time.deltaTime);
        }
        else if (ctrlHorizontal == 0)
        {
            velX = Mathf.MoveTowards(velX, 0f, deAcceleration * Time.deltaTime);
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
            velX = Mathf.MoveTowards(velX, maxSpeed * direction, acceleration * Time.deltaTime);
        }
        else if (horizontal == 0)
        {
            velX = Mathf.MoveTowards(velX, 0f, deAcceleration * Time.deltaTime);
        }

        //velX = Mathf.Clamp(velX, -movementSettings.maxSpeed, movementSettings.maxSpeed);
        Vector2 velocity = new Vector2(velX, rb.velocity.y);

        return velocity;
    }
}