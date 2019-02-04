using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveToTrigger : MonoBehaviour
{
    public event Action<GameObject> OnMove;
    protected void TriggerMove(GameObject obj) { OnMove(obj); }
}

public class MoveToCheckpoint : MonoBehaviour {

    [SerializeField]
    MoveToTrigger MoveTrigger;

    public Transform checkpoint;

	// Use this for initialization
	void Start () {
        MoveTrigger.OnMove += Move;
	}

    private void Move(GameObject obj)
    {
        transform.position = checkpoint.position;
    }

}
