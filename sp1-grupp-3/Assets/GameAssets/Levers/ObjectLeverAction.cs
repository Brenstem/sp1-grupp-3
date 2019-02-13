using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLeverAction : MonoBehaviour
{
    public enum ObjectAction { DoNothing, Move, ActivateComponent, SetVariables }
    public ObjectAction objectAction;

    private bool actionPerformed = false;
    
    public void OnActivatedByLever()
    {
        actionPerformed = true;

        switch (objectAction) {            
            case ObjectAction.Move:

                break;
            case ObjectAction.ActivateComponent:

                break;
            case ObjectAction.SetVariables:

                break;
            default:
                break;
        }
    }

    private void MoveObject()
    {
        Vector3.MoveTowards();
    }

    
}
