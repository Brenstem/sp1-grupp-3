using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerChristoffer : PlayerMovementChristoffer
{
    public float maxSpeed = 7;
    public float jumpForce = 7;
    public float acceleration = 1;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float currentSpeed;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded) {
            velocity.y = jumpForce;
        }
        else if (Input.GetButtonUp("Jump")) {
            if (velocity.y > 0)
                velocity.y = velocity.y * 0.5f;
        }

        bool flipSprite = spriteRenderer.flipX ? (move.x < -0.01f) : (move.x > 0.01f);

        if (flipSprite) {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x / maxSpeed));

        targetVelocity = move * maxSpeed;
    }

    // Behöver implementeras som gravity/sensitivity
    public void UpdateSpeed()
    {
        if (Input.GetAxisRaw("Horizontal") == 1) {
            currentSpeed += acceleration;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1) {
            currentSpeed -= acceleration;
        }
        else if (currentSpeed > 0){
            currentSpeed -= acceleration;
        }
        else if (currentSpeed < 0) {
            currentSpeed += acceleration;
        }
    }
}
