using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCourier : MonoBehaviour
{
    [SerializeField] private float Speed = 0.2f;
    [SerializeField] private WayPoint StartPoint;

    public MapPath CourierPath;
    public bool IsAwaits { get; private set; } = true;

    private void Start()
    {
        CourierPath = new MapPath();
        CourierPath.TryAddPoint(StartPoint);
    }
}
