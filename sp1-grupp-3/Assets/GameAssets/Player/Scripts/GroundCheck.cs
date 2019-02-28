using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    public bool isWalled = false;
    public bool isCollidingBoxes = false;
    public Vector3 groundCollPosition;
    public Vector3 groundCollSize;
    public Vector3 wallCollPosition;
    public Vector3 wallCollSize;
    public LayerMask collideWithFloorLayer;
    public LayerMask collideWithWallLayer;
    PlayerMovement movement;
    public float slopeFriction;
    Rigidbody2D rb;
    RaycastHit2D hit2;
    bool previousGrounded = false;
    private SoundObject soundObject;

    void Start()
    {
        if (GetComponent<PlayerMovement>() != null)
        {
            movement = GetComponent<PlayerMovement>();
        }
        rb = GetComponent<Rigidbody2D>();

        if(transform.tag == "Box")
        {
            soundObject = GetComponent<SoundObject>();
        }
    }

    void Update()
    {
        //var groundHits = new RaycastHit2D[10];
        //int hitcount = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithFloorLayer }, groundHits);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + groundCollPosition, groundCollSize, 0f, Vector2.zero, 0f, collideWithFloorLayer);

        if (hit == true)
        {
            isGrounded = true;

            if (hit.transform.tag == "JumpThroughtPlatforms")
            {
                if (rb.velocity.y < 0 && (transform.position.y + groundCollPosition.y - (groundCollSize.y / 2)) >= (hit.transform.position.y))
                {
                    //isGrounded = true;
                    hit.transform.GetComponent<BoxCollider2D>().isTrigger = false;
                    hit2 = hit;
                }
            }
        }
        else
        {
            isGrounded = false;
            if (hit2 == true)
            {
                hit2.transform.GetComponent<BoxCollider2D>().isTrigger = true;
                //hit2 = hit;
            }
            // hit.transform.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        //isGrounded = hitcount > 0;

        if (isGrounded == true)
        {
            Vector2 position = transform.position;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            rb.velocity = new Vector2(rb.velocity.x - (hit.normal.x * slopeFriction), rb.velocity.y);

            if (transform.tag == "Box")
            {
                if (previousGrounded == false)
                {
                    if (soundObject != null)
                    {
                        soundObject.PlaySound();
                    }

                    previousGrounded = isGrounded;
                }
            }
        }

        if (movement != null)
        {
            var wallHits = new RaycastHit2D[10];
            int wallHitCount = (Physics2D.BoxCast(transform.position + (wallCollPosition * transform.localScale.x), wallCollSize, 0f, Vector2.zero, new ContactFilter2D { useLayerMask = true, layerMask = collideWithWallLayer }, wallHits));
            isWalled = wallHitCount > 0;

            if (isWalled == true)
            {
                movement.StopMovement();
            }
            else
            {
                movement.ContinueMovement();
            }
        }

        previousGrounded = isGrounded;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + groundCollPosition, groundCollSize);
        Gizmos.DrawWireCube(transform.position + (wallCollPosition * transform.localScale.x), wallCollSize);
    }
}