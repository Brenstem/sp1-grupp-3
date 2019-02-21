using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.5f;
    
    void Update()
    {
        var scrollSpeed_X = 0.1f;
        var scrollSpeed_Y = 0.1f;
        float offsetX = Time.time * scrollSpeedX;
        float offsetY = Time.time * scrollSpeedY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, offsetY);
    }
}
