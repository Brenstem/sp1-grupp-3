using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    #region Fields

    public enum ObjectAction { DoNothing, Move, ActivatePhysics, ActivatePlatform }
    [SerializeField] public ObjectAction objectAction;
    public Vector2 moveTo;
    public Platform platfrom;


    private Vector2 currentPosition;
    private float moveIncrement;

    private bool actionPerformed = false;

    #endregion

    #region Set/Get Functions

    public bool ActionPerformed { get => actionPerformed; private set => actionPerformed = value; }

    #endregion

    #region Internal Methods

    private void Update()
    {
        MoveObject();
    }

    private void MoveObject()
    {
        if (actionPerformed && objectAction == ObjectAction.Move) {
            transform.position = Vector2.MoveTowards(currentPosition, moveTo, moveIncrement);

            moveIncrement += Time.deltaTime * 5;
        }
    }

    private void ActivatePhysics()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null) {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    #endregion

    #region Public Methods

    public void OnActivatedByLever()
    {
        actionPerformed = true;

        switch (objectAction) {
            case ObjectAction.Move:
                currentPosition = transform.position;
                moveIncrement = 0.0f;
                break;

            case ObjectAction.ActivatePhysics:
                ActivatePhysics();
                break;

            case ObjectAction.ActivatePlatform:
                platfrom.move = true;
                break;

            default:
                break;
        }
    }

    #endregion

}
