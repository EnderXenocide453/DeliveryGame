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
        if (_isVibration)
        {
            Handheld.Vibrate();
            Debug.Log("Long vibration");
        }
    }
    public static void SingleVibration()
    {
        Handheld.Vibrate();
        Debug.Log("Vibrate worked");
    }
    public static void LongVibration(float time)
    {
        timeOfVibration = time;

        instance.StopAllCoroutines();
        instance.StartCoroutine(instance.VibrateForTime());
    }
    private IEnumerator VibrateForTime()
    {
        _isVibration = true;
        yield return new WaitForSeconds(timeOfVibration);
        _isVibration = false;    
    }
}
