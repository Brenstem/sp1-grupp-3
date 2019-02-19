using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private float menuTimer;
    private float onTransition;

    [FMODUnity.EventRef]
    public string menuMusicSound;
    FMOD.Studio.EventInstance menuMusic;

    void Start()
    {
        PlaySound(sounds.menuMusic);
        menuTimer = 0f;
        onTransition = 0f;
    }

    public void PlaySound(sounds sound)
    {
        menuMusic = FMODUnity.RuntimeManager.CreateInstance(menuMusicSound);
        menuMusic.start();
        menuMusic.release();
    }

    public void Update()
    {
        MenuTransition();
    }

    private void MenuTransition()
    {
        menuTimer += Time.deltaTime;

        if (menuTimer > 298.0f)
        {
            onTransition = 1f;
            menuMusic.setParameterValue("Menu Timer", onTransition);
        }
    }
}
