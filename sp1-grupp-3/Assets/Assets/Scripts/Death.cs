using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MoveToTrigger
{

    [SerializeField]
    float MinYHeight;

    // Update is called once per frame
    void LateUpdate () {
        if (transform.position.y < MinYHeight)
            TriggerMove(gameObject);
	}
}
