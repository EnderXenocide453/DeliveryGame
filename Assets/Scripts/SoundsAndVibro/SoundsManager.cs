using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance = null;
    private static AudioSource _source;

    public AudioClip orderGoodsSound;
    public AudioClip newOrderSound;
    public AudioClip getCashSound;
    public AudioClip footstepSound;
    public AudioClip keyboardSound;
    public AudioClip buildProgressSound;
    public AudioClip buildEndSound;
    public AudioClip buttonSound;
    public AudioClip bonusSound;
    public AudioClip kissSound;

    private void Awake()
    {
        if (instance) {
            Destroy(gameObject);
            return;
        } if (instance == this) {
            return;
        }

        instance = this;
        _source = GetComponent<AudioSource>();
    }

    public static void PlayButtonSound()
    {
        PlaySound(instance.buttonSound);
    }

    public static void PlaySound(AudioClip soundEffect, float volume = 1)
    {
        if (_source.enabled) {
            _source.volume = volume * SettingsManager.Volume;
            _source.PlayOneShot(soundEffect);
        }
    }
}
