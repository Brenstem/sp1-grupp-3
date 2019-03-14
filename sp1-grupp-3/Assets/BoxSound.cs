using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSound : MonoBehaviour
{
    GroundCheckAdvanced gAdvanced;
    public SoundObject landSound, dragSound;
    bool hasPlayedDraggedSound = false;
    bool previousRight = false, previousLeft = false, previousUp = false, previousDown = false;

    void Start()
    {
        gAdvanced = GetComponent<GroundCheckAdvanced>();
    }

    void Update()
    {
        if(gAdvanced.HasLanded(Vector2.down, ref previousDown))
        {
            landSound.PlayOneShotObject();
        }
        if(gAdvanced.HasLanded(Vector2.right, ref previousRight))
        {
            landSound.PlayOneShotObject();
        }
        if(gAdvanced.HasLanded(Vector2.left, ref previousLeft))
        {
            landSound.PlayOneShotObject();
        }
        if(gAdvanced.HasLanded(Vector2.up, ref previousUp))
        {
            landSound.PlayOneShotObject();
        }

        //if (gAdvanced.HasHitWall() == true)
        //{
        //    landSound.PlayOneShotObject();
        //}

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