using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    [SerializeField] GameObject selectOnStart;

    private EventSystem canvasEventSystem;
    private bool buttonSelected = false;
    
    [FMODUnity.EventRef]
    public string onSelectSound;
    FMOD.Studio.EventInstance onSelect;

    [FMODUnity.EventRef]
    public string onStartSound;
    FMOD.Studio.EventInstance onStart;

    private void Start()
    {
        canvasEventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // checks for keyboard/gamepad input and selects menu buttons according to movement
    private void Update()
    {

        if (Input.GetAxis("Vertical") != 0 && !buttonSelected)
        {
            canvasEventSystem.SetSelectedGameObject(selectOnStart);
            buttonSelected = true;
        }

        if (canvasEventSystem.IsPointerOverGameObject())
        {
            buttonSelected = false;
            canvasEventSystem.SetSelectedGameObject(null);
        }

        if (this.gameObject == canvasEventSystem.currentSelectedGameObject)
        {
            PlaySound(sounds.Select);
        }
        Debug.Log(canvasEventSystem.currentSelectedGameObject);

    }

    private void OnDisable()
    {
        buttonSelected = false;
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
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

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
                break;
        }

        onSelect.start();
        onSelect.release();
    }
}
