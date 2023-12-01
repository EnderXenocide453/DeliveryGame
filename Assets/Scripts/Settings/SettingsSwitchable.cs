using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsSwitchable : MonoBehaviour
{
    [SerializeField] SettingType settingType;
    [SerializeField] Behaviour[] controlledObjects;

    private void Awake()
    {
        ApplySettings(settingType);

        SettingsManager.onSettingChanged += ApplySettings;
    }

    private void OnDestroy()
    {
        SettingsManager.onSettingChanged -= ApplySettings;
    }

    private void ApplySettings(SettingType type)
    {
        if (type == settingType) {
            foreach (var obj in controlledObjects) {
                obj.enabled = SettingsManager.GetSettings(type);
            }
        }
    }
}
