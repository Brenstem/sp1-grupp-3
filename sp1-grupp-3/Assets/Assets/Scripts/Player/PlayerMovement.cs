using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public MovementSettings movementSettings;
    Rigidbody2D rb;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
       
	}
	
	void Update ()
    {
        float horizontal = Input.GetAxis("Horizontal");
        horizontal *= movementSettings.startSpeed;
        horizontal = Mathf.Clamp(horizontal, -movementSettings.maxSpeed, movementSettings.maxSpeed);

        rb.velocity = new Vector2(horizontal, rb.velocity.y);
    }
}