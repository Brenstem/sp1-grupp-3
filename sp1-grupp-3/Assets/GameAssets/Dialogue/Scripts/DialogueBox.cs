using UnityEngine;

[ExecuteInEditMode]
public class DialogueBox : MonoBehaviour
{
    public GameObject speaker;
    public Vector2 boxOffset;
    public Vector2 triggerOffset;
    public Vector2 triggerSize;


    private void Update()
    {
        PositionUpdate();
    }


    private void PositionUpdate()
    {
        BoxCollider2D childCollider = GetComponentInChildren<BoxCollider2D>();

        if (speaker != null)
        {
            transform.position = speaker.transform.position + (Vector3)boxOffset;
        }


        childCollider.transform.position = transform.position;
        childCollider.offset = -boxOffset + triggerOffset;
        childCollider.size = triggerSize;
    }

}
