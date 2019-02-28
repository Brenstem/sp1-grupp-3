using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientPlayer : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private bool hasPlayed;
    private FMOD.Studio.EventInstance lastSound;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        sound = FMODUnity.RuntimeManager.CreateInstance(path);

        if (!hasPlayed)
        {
            sound.start();
            hasPlayed = true;
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
