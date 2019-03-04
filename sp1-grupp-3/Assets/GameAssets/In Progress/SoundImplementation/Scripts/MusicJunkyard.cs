using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard : MonoBehaviour
{
    private float onTransition;
    private float toPause;
    private bool hasPlayed;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private void Start()
    {
        onTransition = 0f;
        toPause = 0f;
        hasPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && hasPlayed == false)
        {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            sound.start();
        }
        if (collision.gameObject.CompareTag("Player") && hasPlayed == true)
        {
            Debug.Log("TIllbaka");
            sound.setPaused(false);
            toPause = 0f;
            sound.setParameterValue("To Pause", toPause);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Pause());
        }
    }

    IEnumerator Pause()
    {
        toPause = 10f;
        sound.setParameterValue("To Pause", toPause);
        yield return new WaitForSeconds(5.5f);
        sound.setPaused(true);
        hasPlayed = true;
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
