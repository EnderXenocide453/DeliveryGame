using System.Collections.Generic;
using UnityEngine;

public class GoodsSpawner : InteractableArea
{
    [SerializeField] ProductType SpawnType;
    [SerializeField] Timer timer;

    public override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            GoodsManager.StartSpawnGoodsTo(interactStorage, SpawnType, timer);
        }
    }

    public override void Deactivate(Transform obj)
    {
        
        timer?.StopTimer();
    }
}
