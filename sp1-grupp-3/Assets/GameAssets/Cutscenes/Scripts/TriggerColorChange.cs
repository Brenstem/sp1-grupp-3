using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerColorChange : MonoBehaviour
{
    [SerializeField] string playerTag;
    [SerializeField] bool colorEnabled;

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag(playerTag))
        {
            Camera.main.GetComponent<ColorChangerEffect>().ActivateEffect();
        }
    }
}
