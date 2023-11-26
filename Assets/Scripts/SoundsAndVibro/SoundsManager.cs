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

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        instance = this;
        _source = GetComponent<AudioSource>();

        DontDestroyOnLoad(this);
    }

    public static void PlayButtonSound()
    {
        _source.PlayOneShot(instance.buttonSound);
    }

    public static void PlaySound(AudioClip soundEffect)
    {
        _source.PlayOneShot(soundEffect);
    }
}
