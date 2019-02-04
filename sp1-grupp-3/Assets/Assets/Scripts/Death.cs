using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MoveToTrigger
{
    [SerializeField]
    string objTag;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag(objTag))
            return;

        TriggerMove(gameObject);
    }
}
