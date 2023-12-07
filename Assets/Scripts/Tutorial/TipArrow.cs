using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipArrow : MonoBehaviour
{
    public void SetTarget(Transform target)
    {
        if (!target) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        transform.position = target.position;
    }
}
