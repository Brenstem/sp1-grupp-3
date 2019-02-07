using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] EventSystem canvasEventSystem;
    [SerializeField] GameObject selectOnStart;

    private bool buttonSelected = false;
    private MenuActions menuActions;

    // Reference fetching
    private void Start()
    {
        menuActions = GetComponent<MenuActions>();
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
        Debug.Log(canvasEventSystem);

        menuActions.PlaySound(sounds.Select);
        buttonSelected = false;
        canvasEventSystem.SetSelectedGameObject(null);
    }

    // Checks for when new objects are selected with keyboard/gamepad input to play sound
    public void OnSelect(BaseEventData eventData)
    {
        menuActions.PlaySound(sounds.Select);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }
}
