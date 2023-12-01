using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Timer : MonoBehaviour
{
    [SerializeField] private float updateDelay = 0.02f;

    private Image _progressBar;

    public delegate void TimerEventHandler();
    public event TimerEventHandler onTimeEnds;

    private void Awake()
    {
        _progressBar = GetComponent<Image>();
        _progressBar.fillAmount = 0;
    }

    public void StartTimer(float time, bool reverse = false)
    {
        StartCoroutine(TimerTick(time, reverse));
    }

    public void StopTimer()
    {
        StopAllCoroutines();
        _progressBar.fillAmount = 0;

        onTimeEnds = null;
    }

    private void OnCycleEnds()
    {
        _progressBar.fillAmount = 0;
        onTimeEnds?.Invoke();
    }

    private IEnumerator TimerTick(float time, bool reverse = false, bool repeat = true)
    {
        do {
            for (float currTime = 0; currTime < time; currTime += updateDelay) {
                _progressBar.fillAmount = reverse ? 1 - (currTime / time) : currTime / time;
                yield return new WaitForSeconds(updateDelay);
            }

            OnCycleEnds();
        } while (repeat) ;
    }
}
