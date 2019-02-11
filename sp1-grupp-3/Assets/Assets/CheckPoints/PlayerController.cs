using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;

    [SerializeField]
    public float jumpSpeed;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        var vel = rb.velocity;

        vel.x = Input.GetAxisRaw("Horizontal") * speed;

        if ( Input.GetAxisRaw("Jump") == 1 && Mathf.Approximately(rb.velocity.y, 0))
            vel.y = jumpSpeed;

        rb.velocity = vel;
        
    }
}
