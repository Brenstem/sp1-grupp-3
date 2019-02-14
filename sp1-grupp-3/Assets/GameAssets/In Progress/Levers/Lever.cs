using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public LeverObject[] affectedObjectArray;

    private bool contact = false;

    private void OnTriggerEnter2D(Collider2D collision) { if (TestCollision(collision)) contact = true; }

    private void OnTriggerExit2D(Collider2D collision) { if (TestCollision(collision)) contact = false; }

    private void Update()
    {
        if (Input.GetKey(KeyCode.E) && contact) {
            OnPullLever();
        }
    }

    private void OnPullLever()
    {
        foreach (LeverObject affectedObject in affectedObjectArray) {
            if (!affectedObject.ActionPerformed) {
                affectedObject.OnActivatedByLever();
            }
            
        }
    }

    private bool TestCollision(Collider2D collision)
    {
        return collision.transform.CompareTag("Player");
    }

    private bool TestActivated()
    {
        foreach (LeverObject affectedObject in affectedObjectArray) {
            if (affectedObject.ActionPerformed) {
                Debug.Log("Object has already been activated!");
                return true;
            }
        }

        return false;
    }

    public void ResetLever()
    {

    }
}
