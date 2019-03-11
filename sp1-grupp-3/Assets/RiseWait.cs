using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiseWait : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        Debug.Log(animator.gameObject.GetComponent<PlayerMovement>());
        //animator.gameObject.GetComponent<PlayerMovement>().enabled = false;
        //animator.gameObject.GetComponent<PlayerJump>().enabled = false;

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateinfo, int layerindex)
    {
        //Debug.Log(animator.gameObject.GetComponent<PlayerMovement>());
        //animator.gameObject.GetComponent<PlayerMovement>().enabled = true;
        //animator.gameObject.GetComponent<PlayerJump>().enabled = true;
    }
}
