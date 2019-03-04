using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorChangerEffect : MonoBehaviour
{
    PostProcessVolume postVolume;

    public PostProcessProfile profile;

    public float speed;
    public float startingPoint = 0;
    public float destination;
    bool activateEffect = false;


    void Start()
    {
        postVolume = GetComponent<PostProcessVolume>();
    }

    void Update()
    {
        if(activateEffect == true)
        {
            if(startingPoint != destination)
            {
                startingPoint = Mathf.MoveTowards(startingPoint, destination, speed);
            }
            profile.GetSetting<ColorGrading>().saturation.value = startingPoint; 
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            activateEffect = true;
        }
    }

    public void ActivateEffect()
    {
        activateEffect = true;
    }
}