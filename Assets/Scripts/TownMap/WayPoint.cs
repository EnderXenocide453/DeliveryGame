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
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(GetInstanceID());
    }

    public WayPoint ExtrudePoint()
    {
        WayPoint point = Manager.BuildPoint(transform.position);
        Manager.BuildRoad(this, point);

        return point;
    }

    public void ConnectPoint(WayPoint connectPoint)
    {
        if (Connections.TryAdd(connectPoint.GetInstanceID(), connectPoint))
            connectPoint.onDestroyed += DisconnectPoint;
    }

    public void DisconnectPoint(int id)
    {
        Debug.Log("Disconnected");
        Connections.Remove(id);
    }

    public PointSaveInfo GetSaveInfo()
    {
        PointSaveInfo info = new PointSaveInfo();
        info.position = transform.position;

        info.connections = new List<Vector3>();
        foreach (var connection in Connections.Values)
            info.connections.Add(connection.transform.position);

        return info;
    }
}

[System.Serializable]
public class PointSaveInfo
{
    public Vector3 position;
    public List<Vector3> connections;
}