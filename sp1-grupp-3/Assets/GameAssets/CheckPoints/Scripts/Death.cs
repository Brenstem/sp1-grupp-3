using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : RespawnTrigger
{
    [SerializeField]
    string playerTag;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag(playerTag))
            return;

        TriggerMove(gameObject);
    }
}
