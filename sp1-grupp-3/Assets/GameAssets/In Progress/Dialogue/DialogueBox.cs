using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DialogueBox : MonoBehaviour
{
    public GameObject speaker;
    public Vector2 boxOffset;
    public Vector2 triggerOffset;


    private void Update()
    {
        PositionUpdate();
    }
        

    private void PositionUpdate()
    {
        Collider2D childCollider = GetComponentInChildren<BoxCollider2D>();

        transform.position = speaker.transform.position + (Vector3)boxOffset;

        childCollider.transform.position = transform.position;
        childCollider.offset = -boxOffset + triggerOffset;
    }    
       
}
