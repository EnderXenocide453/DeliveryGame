using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsConsumeArea : InteractableArea
{
    [SerializeField] ProductType[] ConsumeTypes;
    [SerializeField] Timer timer;

    protected override void Activate(Transform obj)
    {
        throw new System.NotImplementedException();
    }

    protected override void Deactivate(Transform obj)
    {
        throw new System.NotImplementedException();
    }
}
