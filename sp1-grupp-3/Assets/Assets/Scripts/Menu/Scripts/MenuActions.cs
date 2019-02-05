using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour, IPointerEnterHandler, ISelectHandler
{
    // Serialized variables
    [SerializeField] GameObject selectOnStart;

    // Private variables
    private EventSystem canvasEventSystem;
    private bool buttonSelected = false;
    private bool keyDown = false;
    
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
        canvasEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Checks for keyboard/gamepad input and switches to keyboard/gamepad control
    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0 && !buttonSelected)
        {
            canvasEventSystem.SetSelectedGameObject(selectOnStart);
            buttonSelected = true;
        }

        // Switches back to mouse control if the player clicks
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            buttonSelected = false;
        }
    }

    // Checks for mouse hovering over buttons
    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(sounds.Select);
        buttonSelected = false;
        canvasEventSystem.SetSelectedGameObject(null);
    }

    // Checks for when new objects are selected with keyboard/gamepad input to play sound
    public void OnSelect(BaseEventData eventData)
    {
        PlaySound(sounds.Select);
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
}
