using UnityEngine;

public class Sounds : MonoBehaviour
{
    public static Sounds instance = null;

    private void Start()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        instance = this;
    }
    public static void PlaySound(AudioClip soundEffect, AudioSource audioSource)
    {
        audioSource.PlayOneShot(soundEffect);
    }
}
