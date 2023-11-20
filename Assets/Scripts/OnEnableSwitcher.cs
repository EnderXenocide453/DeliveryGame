using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBlocker : MonoBehaviour
{
    [SerializeField] Transform[] others;

    private void OnEnable()
    {
        foreach (var obj in others)
            obj.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        foreach (var obj in others)
            obj.gameObject.SetActive(true);
    }
}
