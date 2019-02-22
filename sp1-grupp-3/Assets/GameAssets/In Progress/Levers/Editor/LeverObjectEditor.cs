using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LeverObject)), CanEditMultipleObjects]
public class LeverObjectEditor : Editor
{
    public SerializedProperty actionProperty;

    private LeverObject leverObject;

    private void OnEnable()
    {
        actionProperty = serializedObject.FindProperty("objectAction");
        leverObject = (LeverObject)target;

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        actionProperty.enumValueIndex = EditorGUILayout.Popup(actionProperty.enumValueIndex, actionProperty.enumDisplayNames);

        LeverObject.ObjectAction objectAction = (LeverObject.ObjectAction)actionProperty.enumValueIndex;

        switch (objectAction) {
            case LeverObject.ObjectAction.DoNothing:
                break;
            case LeverObject.ObjectAction.Move:
                leverObject.moveTo = EditorGUILayout.Vector2Field("Move To", leverObject.moveTo);
                break;
            case LeverObject.ObjectAction.ActivatePhysics:
                break;
            case LeverObject.ObjectAction.ActivatePlatform:
                leverObject.platfrom = (Platform)EditorGUILayout.ObjectField("Platform", leverObject.platfrom, typeof(Platform), false);
                break;
            default:
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }

}
