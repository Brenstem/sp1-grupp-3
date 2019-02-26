using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCollision : MonoBehaviour
{
    public bool colliding = false;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            colliding = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag != "Player")
        {
            colliding = false;
        }
    }
}
