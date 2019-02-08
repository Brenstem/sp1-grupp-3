using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    // Serialized variables
    [SerializeField] GameObject creditsHolder;
    [SerializeField] GameObject Title;
    [SerializeField] GameObject buttonHolder;

    // Private variables
    private Animator creditsAnim;
    private Timer creditsTimer;

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

    [FMODUnity.EventRef]
    public string menuMusicSound;
    FMOD.Studio.EventInstance menuMusic;

    // Reference fetching
    private void Start()
    {
        creditsAnim = creditsHolder.GetComponent<Animator>();
        creditsTimer = new Timer();
    }


    private void Update()
    {
        creditsTimer.UpdateTimer();

        if (creditsTimer.TimerFinished || Input.anyKeyDown)
        {
            creditsHolder.SetActive(false);
            buttonHolder.SetActive(true);
            Title.SetActive(true);
        }
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
                onStart = FMODUnity.RuntimeManager.CreateInstance(onStartSound);
                break;
            case sounds.Exit:
                onExit = FMODUnity.RuntimeManager.CreateInstance(onExitSound);
                break;
            case sounds.Select:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onSelectSound);
                break;
            case sounds.menuMusic:
                menuMusic = FMODUnity.RuntimeManager.CreateInstance(menuMusicSound);
                break;
            default:
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
                onExit = FMODUnity.RuntimeManager.CreateInstance(onExitSound);
                break;
            case sounds.Select:
                onSelect = FMODUnity.RuntimeManager.CreateInstance(onSelectSound);
                break;
            case sounds.menuMusic:
                menuMusic = FMODUnity.RuntimeManager.CreateInstance(menuMusicSound);
                break;
            default:
                break;
        }
        onSelect.start();
        onSelect.release();
    }


    // Credits playing
    public void PlayCredits()
    {
        creditsHolder.SetActive(true);
        buttonHolder.SetActive(false);
        Title.SetActive(false);
        creditsTimer.StartTimer(creditsAnim.GetCurrentAnimatorStateInfo(0).length);
    }
}
