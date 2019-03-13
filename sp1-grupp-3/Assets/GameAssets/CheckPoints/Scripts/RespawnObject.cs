using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    public event Action<GameObject> OnMove;
    protected void TriggerMove(GameObject obj) { OnMove(obj); }
}

public class RespawnObject : MonoBehaviour {

    [SerializeField]
    RespawnTrigger RespawnTrigger;

    public Transform checkpoint;
    
	void Start () {
        RespawnTrigger.OnMove += Move;
	}

    private void Move(GameObject obj)
    {
        transform.position = checkpoint.position;
    }
}
