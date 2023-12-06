using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingAudio : MonoBehaviour
{
    [SerializeField] SettingType settingType;
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
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
            _source.mute = !SettingsManager.GetSettings(type);
            _source.volume = SettingsManager.Volume;
        }
    }
}
