using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    Image image;

    private void Start()
    {
        image = transform.parent.GetComponent<Image>();
        image.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        image.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        image.enabled = false;
    }

    private void EnableImage(bool state)
    {
    }
}
