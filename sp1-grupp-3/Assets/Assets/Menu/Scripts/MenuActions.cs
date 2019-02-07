﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Serialized variables
    [SerializeField] GameObject creditsHolder;

    // Private variables
    private Animator creditsAnim;
    private float timer;

    // Sound references
    [FMODUnity.EventRef]
    public string onSelectSound;
    FMOD.Studio.EventInstance onSelect;

    [FMODUnity.EventRef]
    public string onStartSound;
    FMOD.Studio.EventInstance onStart;

    [FMODUnity.EventRef]
    public string onExitSound;
    FMOD.Studio.EventInstance onExit;

    // Reference fetching
    private void Start()
    {
        creditsAnim = creditsHolder.GetComponent<Animator>();
    }

    // Quits game/stops playmode depending on build
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    // Scene loading
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Sound playing
    public void PlaySound(GetEnum sound)
    {
        switch (sound.state)
        {
            case sounds.Play:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onStartSound);
                break;
            case sounds.Exit:
                break;
            case sounds.Select:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onSelectSound);
                break;
            default:
                onExit = FMODUnity.RuntimeManager.CreateInstance(onExitSound);
                break;
        }

        onSelect.start();
        onSelect.release();
    }

    public void PlaySound(sounds sound)
    {
        switch (sound)
        {
            case sounds.Play:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onStartSound);
                break;
            case sounds.Exit:
                break;
            case sounds.Select:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onSelectSound);
                break;
            default:
                onExit = FMODUnity.RuntimeManager.CreateInstance(onExitSound);
                break;
        }

        onSelect.start();
        onSelect.release();
    }

    // Credits playing
    public void PlayCredits()
    {

        timer += Time.deltaTime;
        creditsHolder.SetActive(true);
        Debug.Log(timer);

        if (timer >= creditsAnim.GetCurrentAnimatorStateInfo(0).length)
        {
            Debug.Log("Animation ended");
            //creditsHolder.SetActive(false);
        }
    }
}
