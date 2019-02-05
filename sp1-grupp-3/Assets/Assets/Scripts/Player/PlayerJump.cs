using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public MovementState movementState;
    public MovementState defaultMovementState;

    [SerializeField]
    float jumpStrength;
    [SerializeField]
    float maxJumpHeight;
    [SerializeField]
    float jumpGravity;
    [SerializeField]
    float fallGravity;

    bool jumpRequest;

    GroundCheck gCheck;
    Rigidbody2D rb;

    bool hasBeenGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gCheck = GetComponent<GroundCheck>();
    }

    public void UpdateMovementState(MovementState move)
    {
        jumpStrength = move.jumpStrength;
        maxJumpHeight = move.maxJumpHeight;
        jumpGravity = move.jumpGravity;
        fallGravity = move.fallGravity;
    }

    void Update()
    {
        if (movementState != null)
        {
            UpdateMovementState(movementState);
        }
        else
        {
            UpdateMovementState(defaultMovementState);
        }

        if (hasBeenGrounded == false)
        { hasBeenGrounded = gCheck.isGrounded; }

        bool jumpBtn = Input.GetAxisRaw("Jump") == 1;
        if (jumpBtn == true && hasBeenGrounded == true)
        {
            jumpRequest = true;
            hasBeenGrounded = false;
        }
    }

    void FixedUpdate()
    {
        if (jumpRequest == true)
        {
            // var yVel = Mathf.Sqrt(jumpForce * -Physics2D.gravity.y);
            var yVel = jumpStrength;

            if (rb.velocity.y < 0)
            {
                var vel = rb.velocity;
                vel.y = yVel;
                rb.velocity = vel;
            }
            else
            {
                yVel = Mathf.Clamp(yVel, 0f, maxJumpHeight);
                rb.velocity += new Vector2(0, yVel);

                jumpRequest = false;
            }
        }
        ApplyJumpModifier();
    }

    void ApplyJumpModifier()
    {
        bool jumpBtn = Input.GetAxisRaw("Jump") == 1;

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && jumpBtn == false)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpGravity - 1) * Time.deltaTime;
        }
    }
}