using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard : MonoBehaviour
{
    private float onTransition;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private void Start()
    {
        onTransition = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            sound.start();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Stop(sound);
        }
    }

    private void Stop(FMOD.Studio.EventInstance sound)
    {
        sound.release();
        sound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void Transition()
    {
        onTransition = 1f;
        sound.setParameterValue("Loop1 End", onTransition);
    }
}
