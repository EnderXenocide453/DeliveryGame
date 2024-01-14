using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsConsumeArea : InteractableArea
{
    [SerializeField] ProductType[] ConsumeTypes;
    [SerializeField] Timer timer;

    protected override void OnActivate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            GoodsManager.StartConsumeGoods(interactStorage, ConsumeTypes, timer);
        }
    }

    protected override void OnDeactivate(Transform obj)
    {
        timer?.StopTimer();
    }
}
