using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public LeverObject[] affectedObjectArray;
    public float animationSpeed = 1; 

    private bool contact = false;
    private bool leverActivated = false;

    private void OnTriggerEnter2D(Collider2D collision) { if (TestCollision(collision)) contact = true; }
    private void OnTriggerExit2D(Collider2D collision) { if (TestCollision(collision)) contact = false; }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && contact) {
            OnPullLever();
            
        }
    }

    private void OnPullLever()
    {
        if (!leverActivated) {
            Animator animator = GetComponent<Animator>();
            animator.SetFloat("AnimationSpeedParameter", animationSpeed);
            GetComponent<SoundObject>().PlaySound();

            foreach (LeverObject affectedObject in affectedObjectArray) {
                if (affectedObject != null)
                {
                    if (!affectedObject.ActionPerformed)
                    {
                        
                        affectedObject.OnActivatedByLever();
                    }
                }
            }

            leverActivated = true;
            if (leverActivated == true){
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
