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
            if (activeTutorial)
                timer.onTimeEnds += EndStep;
        }
    }

    public override void Deactivate(Transform obj)
    {
        timer.onTimeEnds -= EndStep;
        timer?.StopTimer();
    }

    public override void EndStep()
    {
        timer.onTimeEnds -= EndStep;
        base.EndStep();
    }
}
