using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    public bool onUpdate;
    public bool onEnable;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance soundObject;
    
    private void Update()
    {
        if (onUpdate == true)
        {
            PlaySound();
        }
    }

    private void OnEnable()
    {
        if (onEnable == true)
        {
            PlaySound();
        }
    }

    public void PlaySound()
    {
        soundObject = FMODUnity.RuntimeManager.CreateInstance(path);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(soundObject, this.transform, this.GetComponent<Rigidbody>());
        soundObject.start();
        soundObject.release();
    }

    public void OnDisable()
    {
        soundObject.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FMODUnity.RuntimeManager.DetachInstanceFromGameObject(soundObject);
    }
}