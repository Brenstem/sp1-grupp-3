using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputAxis
{
    public string name;
    public string descriptiveName;
    public string descriptiveNegativeName;
    public string negativeButton;
    public string positiveButton;
    public string altNegativeButton;
    public string altPositiveButton;

    public float gravity;
    public float dead;
    public float sensitivity;

    public bool snap = false;
    public bool invert = false;

   // public AxisType type;

    public int axis;
    public int joyNum;


    public float maxSpeed;
}