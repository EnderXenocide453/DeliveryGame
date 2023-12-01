using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SettingsSwitcher : MonoBehaviour
{
    [SerializeField] SettingType settingType;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();

        _toggle.isOn = SettingsManager.GetSettings(settingType);
        _toggle.onValueChanged.AddListener(OnSwitched);
    }

    private void OnSwitched(bool on)
    {
        Debug.Log("toggle");
        SettingsManager.SetSettings(settingType, on);
    }
}
