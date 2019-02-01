using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    [SerializeField]
    float jumpForce;
    [SerializeField]
    float jumpHeight;
    [SerializeField]
    float lowJumpMultiplier; //lowJumpMultiplier
    [SerializeField]
    float fallMultiplier;

    bool jumpRequest;

    GroundCheck gCheck;
    Rigidbody2D rb;

    bool hasBeenGrounded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gCheck = GetComponent<GroundCheck>();
    }

    void Update()
    {
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
            var yVel = jumpForce;

            if (rb.velocity.y < 0)
            {
                var vel = rb.velocity;
                vel.y = yVel;
                rb.velocity = vel;
            }
            else
            {
                yVel = Mathf.Clamp(yVel, 0f, jumpHeight);
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
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && jumpBtn == false)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}