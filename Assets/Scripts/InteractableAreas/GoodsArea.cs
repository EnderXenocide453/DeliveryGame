using System.Collections.Generic;
using UnityEngine;

public class GoodsArea : InteractableArea
{
    [SerializeField] bool isImport;
    [SerializeField] Timer timer;

    public Storage ConnectedStorage;

    protected override void OnActivate(Transform obj)
    {
        Debug.Log(activeTutorial);
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            (Storage from, Storage to) = isImport ? (interactStorage, ConnectedStorage) : (ConnectedStorage, interactStorage);

            if (activeTutorial)
                timer.onTimeEnds += EndStep;
            GoodsManager.StartTransportGoods(from, to, timer);
        }
    }

    protected override void OnDeactivate(Transform obj)
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
