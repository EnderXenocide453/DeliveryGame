using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsConsumeArea : InteractableArea
{
    [SerializeField] ProductType[] ConsumeTypes;
    [SerializeField] Timer timer;

    public override void OnActivate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            GoodsManager.StartConsumeGoods(interactStorage, ConsumeTypes, timer);
        }
    }

    public override void OnDeactivate(Transform obj)
    {
        timer?.StopTimer();
    }
}
