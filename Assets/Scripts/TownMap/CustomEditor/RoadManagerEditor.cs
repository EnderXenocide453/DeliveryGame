#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RoadManager))]
public class RoadManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RoadManager manager = (RoadManager)target;

        if (GUILayout.Button("Add point")) {
            Selection.activeObject = manager.BuildPoint(manager.transform.parent.position);
        }
    }
}
#endif