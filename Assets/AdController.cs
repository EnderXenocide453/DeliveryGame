using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AdController : MonoBehaviour
{
    public static AdController instance;

    private static bool _musicState;
    private static bool _soundState;

    [SerializeField] VideoPlayer player;
    [SerializeField] Timer timer;
    [SerializeField] TMPro.TMP_Text counter;
    [SerializeField] string link;

    public delegate void AdEventHandler();
    public static AdEventHandler onAdEnds;

    void Start()
    {
        if (instance) {
            Destroy(this);
            return;
        }

        instance = this;
        gameObject.SetActive(false);

        timer.onTimeEnds += StopAd;
    }

    void Update()
    {
        counter.text = (player.clip.length - timer.currentTime).ToString("0");
    }

    public static void StartAd()
    {
        instance.gameObject.SetActive(true);

        instance.player.frame = 0;
        instance.player.Play();

        instance.timer.StartTimer((float)instance.player.clip.length, false, false);

        _musicState = SettingsManager.Settings[SettingType.Music];
        _soundState = SettingsManager.Settings[SettingType.Sound];

        SettingsManager.SetSettings(SettingType.Music, false);
        SettingsManager.SetSettings(SettingType.Sound, false);
    }

    public static void StopAd()
    {
        instance.gameObject.SetActive(false);

        onAdEnds?.Invoke();
        if (instance.timer.isPlaying)
            instance.timer.StopTimer();

        SettingsManager.SetSettings(SettingType.Music, _musicState);
        SettingsManager.SetSettings(SettingType.Sound, _soundState);
    }

    public void OnClick()
    {

    }
}
