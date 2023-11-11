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
            Selection.activeObject = manager.BuildPoint(Vector3.zero);
        }
    }
}