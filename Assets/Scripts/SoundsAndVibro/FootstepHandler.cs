using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FootstepHandler : MonoBehaviour
{
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound()
    {
        _source.PlayOneShot(SoundsManager.instance.footstepSound);
    }
}
