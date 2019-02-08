using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CanEditMultipleObjects]
[ExecuteInEditMode]
[CustomEditor(typeof(PlayerController))]
public class PlayerMovementEditor : Editor
{
    public static InputAxis inputSettings;
    public static MovementSettings MovementSettings;
    public static string axisString;
    public static int inputIndex;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDefaultInspector();

        PlayerController playerM = (PlayerController)target;
        //PlayerJump playerJ = playerM.GetComponent<PlayerJump>();
        //MovementSettings = playerM.GetComponent<PlayerMovement>().movementSettings;

        if(EditorApplication.isPlaying == false)
        {
            if (playerM.newMovementState != null && playerM.enableNewMovement == true)
            {
                playerM.UpdateMovementState(playerM.newMovementState);
            }
            else if (playerM.newMovementState == null || playerM.newMovementState != null && playerM.enableNewMovement == false)
            {
                playerM.UpdateMovementState(playerM.defaultMovementState);
            }

            //if (playerJ.newMovementState == null)
            //{
            //    playerJ.UpdateMovementState(playerJ.defaultMovementState);
            //}
            //else if (playerJ.newMovementState != null)
            //{
            //    playerJ.UpdateMovementState(playerJ.newMovementState);
            //}
        }
        
        //if (AxisDefined(MovementSettings.name) == true)
        //{
        //    //GetAxis(MovementSettings);
        //}

        serializedObject.ApplyModifiedProperties();
    }

    private static bool AxisDefined(string axisName)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            if (axis.stringValue == axisName) return true;
        }
        return false;
    }
    private static int AxisFound(string axisName)
    {
        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        axesProperty.Next(true);
        axesProperty.Next(true);
        while (axesProperty.Next(false))
        {
            SerializedProperty axis = axesProperty.Copy();
            axis.Next(true);
            if (axis.stringValue == axisName) return axis.CountInProperty();
        }
        return 0;
    }
    private static SerializedProperty GetChildProperty(SerializedProperty parent, string name)
    {
        SerializedProperty child = parent.Copy();
        child.Next(true);
        do
        {
            if (child.name == name) return child;
        }
        while (child.Next(false));
        return null;
    }
    private static void AddAxis(InputAxis axis)
    {
        if (AxisDefined(axis.name) == false) return;

        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        //axesProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(axesProperty.arraySize - 1);

        GetChildProperty(axisProperty, "m_Name").stringValue = axis.name;
        GetChildProperty(axisProperty, "descriptiveName").stringValue = axis.descriptiveName;
        GetChildProperty(axisProperty, "descriptiveNegativeName").stringValue = axis.descriptiveNegativeName;
        GetChildProperty(axisProperty, "negativeButton").stringValue = axis.negativeButton;
        GetChildProperty(axisProperty, "positiveButton").stringValue = axis.positiveButton;
        GetChildProperty(axisProperty, "altNegativeButton").stringValue = axis.altNegativeButton;
        GetChildProperty(axisProperty, "altPositiveButton").stringValue = axis.altPositiveButton;
        GetChildProperty(axisProperty, "gravity").floatValue = axis.gravity;
        GetChildProperty(axisProperty, "dead").floatValue = axis.dead;
        GetChildProperty(axisProperty, "sensitivity").floatValue = axis.sensitivity;
        GetChildProperty(axisProperty, "snap").boolValue = axis.snap;
        GetChildProperty(axisProperty, "invert").boolValue = axis.invert;
        //GetChildProperty(axisProperty, "type").intValue = (int)axis.type;
        GetChildProperty(axisProperty, "axis").intValue = axis.axis - 1;
        GetChildProperty(axisProperty, "joyNum").intValue = axis.joyNum;

        serializedObject.ApplyModifiedProperties();
    }
    private static void GetAxis(MovementSettings move)
    {
        //if (AxisDefined(move.name) == false) return;

        SerializedObject serializedObject = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0]);
        SerializedProperty axesProperty = serializedObject.FindProperty("m_Axes");

        //axesProperty.arraySize++;
        serializedObject.ApplyModifiedProperties();

        SerializedProperty axisProperty = axesProperty.GetArrayElementAtIndex(0);

        //GetChildProperty(axisProperty, "m_Name").stringValue = move.name;
        GetChildProperty(axisProperty, "gravity").floatValue = move.deAcceleration;
        GetChildProperty(axisProperty, "sensitivity").floatValue = move.acceleration;

        //GetChildProperty(axisProperty, "m_Name").stringValue = move.name;
        //GetChildProperty(axisProperty, "gravity").floatValue = move.deAcceleration;
        //GetChildProperty(axisProperty, "sensitivity").floatValue = move.acceleration;

        serializedObject.ApplyModifiedProperties();
    }
}

public enum AxisType
{
    KeyOrMouseButton = 0,
    MouseMovement = 1,
    JoystickAxis = 2
};

