using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {
    public bool trigger;
    public event Action<Checkpoint> OnTrigger;
    public event Action<Checkpoint> OnRespawn;


    void Awake () {
        trigger = false;
	}
	
	void  Update() {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //trigga en gång (ta bort för quicksave?)
        if (!trigger) {
            //Kolla så att vi endast kolliderar med spelaren. Layer = Checkpoint och Physics
            if (GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer("Player"))
                ;
        }
    }

    public void Trigger(Collider2D collider)
    {
        PlayerDeath player = collider.GetComponent<PlayerDeath>();
        player.onDeath += OnCharactedDeath;

        GetComponent<Animator>().SetTrigger("Trigger");

        trigger = true;
    }

    void OnCharactedDeath()
    {
        if (CheckpointManager.Instance.currentCheckPoint == this)
            OnRespawn.Invoke(this);
    }
}
