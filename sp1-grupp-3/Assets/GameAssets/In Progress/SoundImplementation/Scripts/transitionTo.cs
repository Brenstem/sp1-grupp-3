using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionTo : MonoBehaviour
{
    public MusicJunkyard music;

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            music.Transition();
        }
    }
}
