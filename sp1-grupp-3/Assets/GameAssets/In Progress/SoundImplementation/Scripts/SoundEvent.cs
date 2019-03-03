using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEvent : MonoBehaviour
{
    //Loop instance
    public string path;
    FMOD.Studio.EventInstance loopSound;

    void PlayOneShot(string path)
    {
        FMODUnity.RuntimeManager.PlayOneShot(path, GetComponent<Transform>().position);
    }

    void PlayLoopable(string path)
    {
        loopSound = FMODUnity.RuntimeManager.CreateInstance(path);
        loopSound.start();
    }

    void OnDestroyLoop(string path)
    {
        loopSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        loopSound.release();
    }
}