using System.Collections.Generic;
using UnityEngine;

public class GoodsSpawner : InteractableArea
{
    [SerializeField] ProductType SpawnType;
    [SerializeField] Timer timer;

    protected override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            GoodsManager.StartSpawnGoodsTo(interactStorage, SpawnType, timer);
        }
    }

    protected override void Deactivate(Transform obj)
    {
        
        timer?.StopTimer();
    }
}
