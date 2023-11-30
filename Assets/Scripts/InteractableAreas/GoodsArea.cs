using System.Collections.Generic;
using UnityEngine;

public class GoodsArea : InteractableArea
{
    [SerializeField] bool isImport;
    [SerializeField] Timer timer;

    public Storage ConnectedStorage;

    private Dictionary<int, Coroutine> _activeCoroutines;

    private void Start()
    {
        if (!ConnectedStorage) {
            Debug.LogWarning("Не подключено хранилище!");
            Destroy(this);
        }

        _activeCoroutines = new Dictionary<int, Coroutine>();
    }

    protected override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            (Storage from, Storage to) = isImport ? (interactStorage, ConnectedStorage) : (ConnectedStorage, interactStorage);

            Coroutine coroutine = GoodsManager.TransportGoods(from, to, timer);
            
            if (coroutine != null) {
                _activeCoroutines.Add(obj.GetInstanceID(), coroutine);
            }
        }
    }

    protected override void Deactivate(Transform obj)
    {
        int id = obj.GetInstanceID();

        if (_activeCoroutines.TryGetValue(id, out var coroutine)) {
            StopCoroutine(coroutine);
            timer.StopTimer();
            _activeCoroutines.Remove(id);
        }
    }
}
