#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerUpgradeQueue))]
public class UpgradeQueueCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var queue = (PlayerUpgradeQueue)target;
        if (GUILayout.Button("Улучшить")) {
            queue.CurrentUpgrade?.DoUpgrade();
        }
    }
}
#endif