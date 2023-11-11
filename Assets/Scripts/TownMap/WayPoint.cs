using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public Dictionary<int, WayPoint> Connections;
    public RoadManager Manager;

    public delegate void WayPointHandler(int id);
    public event WayPointHandler onDestroyed;

    private void Awake()
    {
        Connections = new Dictionary<int, WayPoint>();
        RoadManager.AddPoint(this);
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(gameObject.GetInstanceID());
    }

    public WayPoint ExtrudePoint()
    {
        WayPoint point = Manager.BuildPoint(transform.position);
        Manager.BuildRoad(this, point);

        return point;
    }

    public void ConnectPoint(WayPoint connectPoint)
    {
        if (Connections.TryAdd(connectPoint.gameObject.GetInstanceID(), connectPoint))
            connectPoint.onDestroyed += DisconnectPoint;

        Debug.Log("Connected!");
    }

    public void DisconnectPoint(int id)
    {
        Connections.Remove(id);

        Debug.Log("Disconnected!");
    }
}