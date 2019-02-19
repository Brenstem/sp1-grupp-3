using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    public GameObject speaker;
    public Vector2 boxOffset;
    public Vector2 padding;

    private Canvas canvas;
    private RectTransform rectTransform;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Vector2 childSpriteSize = GetComponentInChildren<RectTransform>().sizeDelta;

        rectTransform.sizeDelta = childSpriteSize + padding;

        transform.position = speaker.transform.position + (Vector3)boxOffset;
    }
}
