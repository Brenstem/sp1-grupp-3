using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderClimb : MonoBehaviour
{
    [Header("Add tiles as children to this gameobject. Do not give the tiles colliders.")]

    [SerializeField] float climbSpeed;
    [SerializeField] string playerTag;
    [SerializeField] GameObject attachedPlatform; 

    private GameObject player;
    private Vector2 moveDir;


    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(0, Input.GetAxisRaw("Vertical"));

        if (player != null)
        {
            if (player.GetComponent<GroundCheck>().isGrounded)
            {
                player.GetComponent<PlayerMovement>().enabled = true;
                player.GetComponent<PlayerJump>().enabled = true;
                attachedPlatform.GetComponent<BoxCollider2D>().enabled = true;
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        } 
    }

    private void OnTriggerStay2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            player = hitInfo.gameObject;
            if (!player.GetComponent<GroundCheck>().isGrounded)
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                player.GetComponent<PlayerJump>().enabled = false;
                attachedPlatform.GetComponent<BoxCollider2D>().enabled = false;
                player.GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            player.GetComponent<Rigidbody2D>().velocity = moveDir * climbSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D hitInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerJump>().enabled = true;
        attachedPlatform.GetComponent<BoxCollider2D>().isTrigger = true;
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

    }
}
