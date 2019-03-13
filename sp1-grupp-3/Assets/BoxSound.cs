using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour
{
    GroundCheckAdvanced gAdvanced;
    public SoundObject landSound, dragSound;
    bool hasPlayedDraggedSound = false;

    void Start()
    {
        gAdvanced = GetComponent<GroundCheckAdvanced>();
    }

    void Update()
    {
        if(gAdvanced.HasLanded() == true)
        {
            landSound.PlayOneShotObject();
        }

        if (gAdvanced.HasHitWall() == true)
        {
            landSound.PlayOneShotObject();
        }

        if (gAdvanced.IsDraggedOnGround() == true)
        {
            if (hasPlayedDraggedSound == false)
            {
                dragSound.PlayLoopObject();
                hasPlayedDraggedSound = true;
            }
        }
        else
        {
            hasPlayedDraggedSound = false;
        }
    }
}