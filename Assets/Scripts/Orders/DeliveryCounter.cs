using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [SerializeField] LayerMask CourierMask;
    [SerializeField] LayerMask CladoOneMask;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & CourierMask) > 0 && other.attachedRigidbody.TryGetComponent<Courier>(out var courier)) {
            CourierManager.SetAwaitingCourier(courier);
        }
        else if (((1 << other.gameObject.layer) & CladoOneMask) > 0 && other.attachedRigidbody.TryGetComponent<Storage>(out var storage)) {
            CourierManager.SendGoodsToAwaiting(storage);
        }
    }
}
