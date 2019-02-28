using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicJunkyard : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance sound;

    private void OnTriggerEnter2D()
    {
        sound = FMODUnity.RuntimeManager.CreateInstance(path);
        sound.start();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Stop(sound);   
    }

    private void Stop(FMOD.Studio.EventInstance soundToStop)
    {
        soundToStop.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundToStop.release();
    }
}
