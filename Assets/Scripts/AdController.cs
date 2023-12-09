using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AdController : MonoBehaviour
{
    public static AdController instance;

    private static bool _musicState;
    private static bool _soundState;

    private static bool _adStarted;

    [SerializeField] VideoPlayer player;
    [SerializeField] Image progressBar;
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
    }

    void LateUpdate()
    {
        if (!_adStarted)
            return;

        counter.text = (player.clip.length - player.clockTime).ToString("0");
        progressBar.fillAmount = (float)(1 - player.clockTime / player.clip.length);

        if (!player.isPlaying)
            StopAd();
    }

    public static void StartAd()
    {
        instance.gameObject.SetActive(true);

        instance.player.frame = 0;
        instance.player.Play();

        _musicState = SettingsManager.Settings[SettingType.Music];
        _soundState = SettingsManager.Settings[SettingType.Sound];

        SettingsManager.SetSettings(SettingType.Music, false);
        SettingsManager.SetSettings(SettingType.Sound, false);

        _adStarted = true;
    }

    public static void StopAd()
    {
        instance.gameObject.SetActive(false);

        onAdEnds?.Invoke();

        SettingsManager.SetSettings(SettingType.Music, _musicState);
        SettingsManager.SetSettings(SettingType.Sound, _soundState);

        _adStarted = false;
    }

    public void OpenLink()
    {
        Application.OpenURL(link);
    }
}
