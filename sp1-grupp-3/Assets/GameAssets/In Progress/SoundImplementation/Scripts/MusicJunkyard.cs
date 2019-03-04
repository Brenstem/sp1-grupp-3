using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private bool hasPlayed = true;

    private void OnTriggerEnter2D()
    {
        if (hasPlayed)
        {
            sound = FMODUnity.RuntimeManager.CreateInstance(path);
            sound.start();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Stop(sound);   
    }

    private void Stop(FMOD.Studio.EventInstance soundToStop)
    {
        soundToStop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundToStop.release();
        hasPlayed = false;
    }
}
