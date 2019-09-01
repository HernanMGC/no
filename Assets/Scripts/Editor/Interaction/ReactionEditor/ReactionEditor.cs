using System;
using UnityEngine;
using UnityEditor;

public abstract class ReactionEditor : Editor
{
    public bool showReaction;
    public SerializedProperty reactionsProperty;


    private Reaction reaction;


    private const float buttonWidth = 30f;


    private void OnEnable()
    {
        reaction = (Reaction)target;
        Init();
    }


    protected virtual void Init() { }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.BeginVertical(GUI.skin.box);
        EditorGUI.indentLevel++;

        EditorGUILayout.BeginHorizontal();

        int currentIndex;
        showReaction = EditorGUILayout.Foldout(showReaction, GetFoldoutLabel());

        if (GUILayout.Button("-", GUILayout.Width(buttonWidth)))
        {
            reactionsProperty.RemoveFromObjectArray(reaction);
        }
        if (GUILayout.Button("^", GUILayout.Width(buttonWidth)))
        {
            currentIndex = reactionsProperty.GetIndexOfObjectInArray(reaction);
            //reactionsProperty.RemoveFromObjectArrayAt(currentIndex);
            //reactionsProperty.AddToObjectArrayAt(reaction, currentIndex - 1);
            reactionsProperty.MoveObjectInArray(currentIndex, currentIndex - 1);
        }
        if (GUILayout.Button("v", GUILayout.Width(buttonWidth)))
        {
            currentIndex = reactionsProperty.GetIndexOfObjectInArray(reaction);
            //reactionsProperty.RemoveFromObjectArrayAt(currentIndex);
            //reactionsProperty.AddToObjectArrayAt(reaction, currentIndex + 1);
            reactionsProperty.MoveObjectInArray(currentIndex, currentIndex + 1);
        }
        EditorGUILayout.EndHorizontal();

        if (showReaction)
        {
            DrawReaction();
        }

        EditorGUI.indentLevel--;
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }


    public static Reaction CreateReaction(Type reactionType)
    {
        return (Reaction)CreateInstance(reactionType);
    }


    protected virtual void DrawReaction()
    {
        DrawDefaultInspector();
    }


    protected abstract string GetFoldoutLabel();
}
