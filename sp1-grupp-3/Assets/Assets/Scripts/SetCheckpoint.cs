using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoint : MonoBehaviour {

    [SerializeField]
    string objTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.transform.CompareTag(objTag))
            return;

        var respawn = collision.GetComponent<MoveToCheckpoint>();

        if (respawn == null)
            return;


        if (respawn.checkpoint != null && respawn.checkpoint.position.x > transform.position.x)
            return;

        respawn.checkpoint = transform;

    }





}
