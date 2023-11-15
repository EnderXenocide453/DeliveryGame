using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CourierManager))]
public class CourierManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CourierManager manager = (CourierManager)target;
        if (GUILayout.Button("Добавить нового курьера"))
            manager.AddNewCourier();
        if (GUILayout.Button("Продвинуть очередь"))
            manager.RemoveCourierFromQueue();
    }
}
