using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsConsumeArea : InteractableArea
{
    [SerializeField] ProductType[] ConsumeTypes;
    [SerializeField] Timer timer;

    public override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            GoodsManager.StartConsumeGoods(interactStorage, ConsumeTypes, timer);
        }
    }

    public override void Deactivate(Transform obj)
    {
        timer?.StopTimer();
    }
}
