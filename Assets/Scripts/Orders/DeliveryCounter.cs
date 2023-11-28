using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [SerializeField] LayerMask CourierMask;
    [SerializeField] LayerMask StorekeeperMask;

    private Storage _storekeeperStorage;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & CourierMask) > 0 && other.attachedRigidbody.TryGetComponent<Courier>(out var courier)) {
            CourierManager.SetAwaitingCourier(courier);
            OrdersManager.GenerateOrderForCourier(courier);

            if (_storekeeperStorage)
                CourierManager.SendGoodsToAwaiting(_storekeeperStorage);
        }
        else if (((1 << other.gameObject.layer) & StorekeeperMask) > 0 && other.attachedRigidbody.TryGetComponent<Storage>(out var storage)) {
            _storekeeperStorage = storage;
            CourierManager.SendGoodsToAwaiting(storage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & StorekeeperMask) > 0 && other.attachedRigidbody.TryGetComponent<Storage>(out var storage))
            _storekeeperStorage = null;
    }
}
