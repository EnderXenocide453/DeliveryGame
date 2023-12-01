using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static Dictionary<SettingType, bool> _settings;

    public static Dictionary<SettingType, bool> Settings { get => _settings == null ? (_settings = new Dictionary<SettingType, bool>()) : _settings; }

    public delegate void SettingsEventHandler(SettingType type);
    public static event SettingsEventHandler onSettingChanged;

    private void Awake()
    {
        LoadSettings();
        //DontDestroyOnLoad(this);
    }

    public static void SetSettings(SettingType type, bool state)
    {
        if (!Settings.TryAdd(type, state))
            Settings[type] = state;

        onSettingChanged?.Invoke(type);

        SaveSettings();
    }

    public static bool GetSettings(SettingType type)
    {
        return Settings[type];
    }

    public static void SaveSettings()
    {
        try {
            var save = new SettingsSaveStruct();
            save.music = Settings[SettingType.Music];
            save.sound = Settings[SettingType.Sound];
            save.vibro = Settings[SettingType.Vibro];

            string json = JsonUtility.ToJson(save, true);
            string savePath = "";

#if UNITY_ANDROID && !UNITY_EDITOR
            savePath = Path.Combine(Application.persistentDataPath, "config");
#else
            savePath = Path.Combine(Application.dataPath, "config");
#endif

            File.WriteAllText(savePath, json);
        } catch {
            Debug.LogWarning("Ошибка сохранения!");
        }
    }

    public static void LoadSettings()
    {
        Debug.Log("Загрузка настроек");
        string savePath = "";

#if UNITY_ANDROID && !UNITY_EDITOR
        savePath = Path.Combine(Application.persistentDataPath, "config");
#else
        savePath = Path.Combine(Application.dataPath, "config");
#endif

        if (!File.Exists(savePath)) {
            _settings = new Dictionary<SettingType, bool>();
            _settings[SettingType.Music] = true;
            _settings[SettingType.Sound] = true;
            _settings[SettingType.Vibro] = true;

            return;
        }

        try {
            string json = File.ReadAllText(savePath);
            var save = JsonUtility.FromJson<SettingsSaveStruct>(json);

            _settings = new Dictionary<SettingType, bool>();
            _settings[SettingType.Music] = save.music;
            _settings[SettingType.Sound] = save.sound;
            _settings[SettingType.Vibro] = save.vibro;
        }
        catch (Exception e) {
            Debug.LogWarning($"Ошибка загрузки!\n{e}");
        }
    }

    private struct SettingsSaveStruct
    {
        public bool music;
        public bool sound;
        public bool vibro;
    }
}

public enum SettingType
{
    Music,
    Sound,
    Vibro
}