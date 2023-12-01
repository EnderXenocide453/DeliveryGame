using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfterStart : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }
}
