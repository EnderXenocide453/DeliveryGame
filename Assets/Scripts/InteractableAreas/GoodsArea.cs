using System.Collections.Generic;
using UnityEngine;

public class GoodsArea : InteractableArea
{
    [SerializeField] bool isImport;
    [SerializeField] Timer timer;

    public Storage ConnectedStorage;

    private Coroutine _activeCoroutine;

    private void Start()
    {
        if (!ConnectedStorage) {
            Debug.LogWarning("Не подключено хранилище!");
            Destroy(this);
        }
    }

    public override void Activate(Transform obj)
    {
        Debug.Log(activeTutorial);
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            (Storage from, Storage to) = isImport ? (interactStorage, ConnectedStorage) : (ConnectedStorage, interactStorage);

            if (activeTutorial)
                timer.onTimeEnds += EndStep;
            GoodsManager.StartTransportGoods(from, to, timer);
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
