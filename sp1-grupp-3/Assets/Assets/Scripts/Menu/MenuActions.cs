using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour {

    [SerializeField] bool selectOnInput;
    [SerializeField] EventSystem canvasEventSystem;
    [SerializeField] GameObject selectOnStart;

    private bool buttonSelected = false;

    // checks for player movement and selects menu buttons according to movement
    private void Update()
    {
        if (selectOnInput)
        {
            if (Input.GetAxis("Vertical") != 0 && !buttonSelected)
            {
                canvasEventSystem.SetSelectedGameObject(selectOnStart);
                buttonSelected = true;
            }
        }
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
}
