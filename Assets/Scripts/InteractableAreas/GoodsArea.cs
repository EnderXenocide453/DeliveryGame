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

    protected override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            (Storage from, Storage to) = isImport ? (interactStorage, ConnectedStorage) : (ConnectedStorage, interactStorage);

            GoodsManager.StartTransportGoods(from, to, timer);
        }
    }

    protected override void Deactivate(Transform obj)
    {
        timer?.StopTimer();
    }
}
