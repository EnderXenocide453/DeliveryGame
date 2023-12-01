using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public bool isBusy { get; private set; }

    private Dictionary<int, InteractableArea> _nearAreas;

    private void Awake()
    {
        _nearAreas = new Dictionary<int, InteractableArea>();
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
        if (_nearAreas.Count > 0)
            _nearAreas.ElementAt(0).Value.Activate(transform);
    }
}
