using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Reflection;


[CustomEditor(typeof(Locker))]
public class LockerEditor : Editor
{
    private IList lockers, onLockChangedListeners;


    private void OnEnable()
    {
        lockers = target.GetType().GetField("lockers", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target) as IList;
        onLockChangedListeners = target.GetType().GetField("onLockChangedListeners", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(target) as IList;
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUI.BeginDisabledGroup(true);
        {
            EditorGUILayout.LabelField("Lockers", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            if (lockers.Count == 0) EditorGUILayout.LabelField("None");
            for (int i = 0; i < lockers.Count; i++)
            {
                EditorGUILayout.ObjectField(lockers[i] as Object, lockers[i].GetType(), true);
            }
            EditorGUI.indentLevel--;

            GUILayout.Space(10);

            EditorGUILayout.LabelField("On Lock Change Listeners", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;

            if (onLockChangedListeners.Count == 0) EditorGUILayout.LabelField("None");
            for (int i = 0; i < onLockChangedListeners.Count; i++)
            {
                EditorGUILayout.ObjectField(onLockChangedListeners[i] as Object, lockers[i].GetType(), true);
            }
            EditorGUI.indentLevel--;
        }
        EditorGUI.EndDisabledGroup();

        Repaint();
    }
}