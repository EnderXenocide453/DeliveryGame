using System.Collections;
using UnityEngine;

public class Vibration : MonoBehaviour 
{
    public static Vibration instance = null;
    private static float timeOfVibration = 0.35f;

    private bool _isVibration;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        instance = this;
    }
    private void Update()
    {
#if UNITY_ANDROID
        if (_isVibration)
        {
            Handheld.Vibrate();
            Debug.Log("Long vibration");
        }
#endif
    }
    public static void SingleVibration()
    {
#if UNITY_ANDROID
        if (!SettingsManager.Settings[SettingType.Vibro])
            return;

        Handheld.Vibrate();
        Debug.Log("Vibrate worked");
#endif
    }
    public static void LongVibration(float time)
    {
#if UNITY_ANDROID
        if (!SettingsManager.Settings[SettingType.Vibro])
            return;

        timeOfVibration = time;

        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.VibrateForTime());
#endif
    }
    private IEnumerator VibrateForTime()
    {
        _isVibration = true;
        yield return new WaitForSeconds(timeOfVibration);
        _isVibration = false;    
    }
}
