using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement movement;
    private PlayerJump jump;
    private GroundCheck groundCheck;
    private PlayerGrab grab;
    private Animator anim;
    
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        jump = GetComponent<PlayerJump>();
        groundCheck = GetComponent<GroundCheck>();
        grab = GetComponent<PlayerGrab>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float moving = 0;

        if(Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            moving = 1;
        }

        anim.SetFloat("Moving", moving);

        anim.SetBool("Grabbed", grab.grabbed);

        anim.SetBool("IsGrounded", groundCheck.isGrounded);
    }
}