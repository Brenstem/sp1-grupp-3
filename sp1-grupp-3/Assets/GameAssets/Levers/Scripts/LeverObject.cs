using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverObject : MonoBehaviour
{
    #region Fields

    public enum ObjectAction { DoNothing, MoveToPosition, MoveByDistance, ActivatePhysics }

    [SerializeField] public ObjectAction objectAction;
    public Vector2 moveTo;
    public Vector2 moveBy;
    public float moveSpeed = 1;

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
        if (actionPerformed && objectAction == ObjectAction.MoveToPosition) {
            transform.position = Vector2.MoveTowards(currentPosition, moveTo, moveIncrement);

            moveIncrement += Time.deltaTime * moveSpeed;
        }

        else if (actionPerformed && objectAction == ObjectAction.MoveByDistance) {
            Vector2 localMovement = moveBy + currentPosition;

            transform.position = Vector2.MoveTowards(currentPosition, localMovement, moveIncrement);

            moveIncrement += Time.deltaTime * moveSpeed;
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
        Platform platform = GetComponent<Platform>();

        actionPerformed = true;

        if (platform == null) {
            switch (objectAction) {
                case ObjectAction.MoveToPosition:
                    currentPosition = transform.position;
                    moveIncrement = 0.0f;
                    break;

                case ObjectAction.MoveByDistance:
                    currentPosition = transform.position;
                    break;

                case ObjectAction.ActivatePhysics:
                    ActivatePhysics();
                    break;

                default:
                    break;
            }
        }
        else {
            platform.move = true;
        }
    }

    #endregion

}
