using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSharer : MonoBehaviour
{
    [SerializeField] Transform[] connectedTransforms;

    private void OnEnable()
    {
        foreach (var obj in connectedTransforms)
            obj.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        foreach (var obj in connectedTransforms)
            obj.gameObject.SetActive(false);
    }
}
