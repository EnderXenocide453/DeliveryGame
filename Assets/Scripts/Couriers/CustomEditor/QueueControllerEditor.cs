using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QueueController))]
public class QueueControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QueueController queue = (QueueController)target;
        if (GUILayout.Button("Обновить очередь")) {
            queue.CreateQueue();
        }
    }
}
