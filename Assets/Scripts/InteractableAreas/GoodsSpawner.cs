using System.Collections.Generic;
using UnityEngine;

public class GoodsSpawner : InteractableArea
{
    [SerializeField] ProductType SpawnType;

    private Dictionary<int, Coroutine> _activeCoroutines;

    private void Start()
    {
        _activeCoroutines = new Dictionary<int, Coroutine>();
    }

    protected override void Activate(Transform obj)
    {
        if (obj.TryGetComponent<Storage>(out var interactStorage)) {
            Coroutine coroutine = GoodsManager.SpawnGoodsTo(interactStorage, SpawnType);

            if (coroutine != null)
                _activeCoroutines.Add(obj.GetInstanceID(), coroutine);
        }
    }

    protected override void Deactivate(Transform obj)
    {
        Debug.Log("сцуко");
        int id = obj.GetInstanceID();

        if (_activeCoroutines.TryGetValue(id, out var coroutine)) {
            StopCoroutine(coroutine);
            _activeCoroutines.Remove(id);
        }
    }
}
