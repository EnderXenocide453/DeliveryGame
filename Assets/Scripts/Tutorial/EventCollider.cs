using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCollider : MonoBehaviour
{
    [SerializeField] UnityEvent triggerEvent;
    [SerializeField] LayerMask playerMask;

    private void Start()
    {
        enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled && ((1 << other.gameObject.layer) & playerMask.value) > 0)
            triggerEvent?.Invoke();
    }
}
