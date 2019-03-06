using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundObject : MonoBehaviour
{
    [SerializeField] bool playOnStart = true;
    [SerializeField] bool onUpdate;
    [SerializeField] bool onEnable;

    [FMODUnity.EventRef]
    public string path;
    FMOD.Studio.EventInstance soundObject;

    void Start()
    {
        if (playOnStart)
        {
            PlaySound();
        }
    }

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