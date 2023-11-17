#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WayPoint)), CanEditMultipleObjects]
public class WayPointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        WayPoint point = (WayPoint)target;
        
        if (GUILayout.Button("Extrude")) {
            Selection.activeObject = point.ExtrudePoint().gameObject;
        }

        if (GUILayout.Button("Connect")) {
            point.Manager.BuildRoadBetweenSelected();
        }
    }
}
#endif