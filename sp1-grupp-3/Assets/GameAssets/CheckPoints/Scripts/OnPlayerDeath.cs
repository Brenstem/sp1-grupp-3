using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlayerDeath : RespawnTrigger {

    //public event Action OnDeath;

    [SerializeField]
    Death playerDeath;
    
    void Start () {
        playerDeath.OnMove += PlayerDied;
	}

    private void PlayerDied(GameObject obj)
    {
        TriggerMove(obj);
        //OnDeath();
    }
}
