using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject[] affectedObjectArray;


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckCollision(collision) && Input.GetKeyDown("Fire1")) {
            OnPullLever();
        }
    }

    private bool CheckCollision(Collision2D collision)
    {
        bool isCollided = collision.transform.CompareTag("Player");

        return isCollided;
    }

    private void OnPullLever()
    {
        foreach (GameObject gameObject in affectedObjectArray) {
            if (gameObject.GetComponent<ObjectLeverAction>() as ObjectLeverAction != null) {
                ObjectLeverAction action = gameObject.GetComponent<ObjectLeverAction>();

                action.OnActivatedByLever();
                action.enabled = false;

            }
            else {
                Debug.Log("No ObjectLeverAction script attached!");
            }
        }
    }

    public void ResetLever()
    {

    }
}
