using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public bool isBusy { get; private set; }

    private Dictionary<int, InteractableArea> _nearAreas;
    private InteractableArea _activeArea;

    private void Awake()
    {
        _nearAreas = new Dictionary<int, InteractableArea>();
    }

    private void FixedUpdate()
    {
        CalculateNearestArea();
    }

    public void AddArea(InteractableArea area)
    {
        _nearAreas.Add(area.GetInstanceID(), area);

        if (_nearAreas.Count == 1)
            area.Activate(transform);
    }

    public void RemoveArea(InteractableArea area)
    {
        _nearAreas.Remove(area.GetInstanceID());
        area.Deactivate(transform);

        if (_nearAreas.Count > 0)
            _nearAreas.ElementAt(0).Value.Activate(transform);

        CalculateNearestArea();
    }

    private void CalculateNearestArea()
    {
        float distance = float.MaxValue;
        int key = -1;

        foreach (var area in _nearAreas) {
            float currentDistance = Vector3.Distance(transform.position, area.Value.transform.position);
            
            if (distance > currentDistance) {
                distance = currentDistance;
                key = area.Key;
            }
        }

        if (key == -1) {
            _activeArea = null;
            return;
        }

        if (_activeArea == _nearAreas[key])
            return;

        _activeArea?.Deactivate(transform);
        _activeArea = _nearAreas[key];
        _activeArea?.Activate(transform);
    }
}
