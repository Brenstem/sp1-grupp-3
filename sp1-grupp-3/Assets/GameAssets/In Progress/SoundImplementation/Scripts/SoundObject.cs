using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance soundObject;

    void Start()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        soundObject = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundObject, this.transform, this.GetComponent<Rigidbody>());
        soundObject.start();
        soundObject.release();
    }

    public void OnDestroy()
    {
        FMODUnity.RuntimeManager.DetachInstanceFromGameObject(soundObject);
    }
}