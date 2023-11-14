using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public Dictionary<int, (WayPoint point, Road road)> Connections;
    public RoadManager Manager;

    private Image _image;

    public delegate void WayPointHandler(int id);
    public event WayPointHandler onDestroyed;
    public event WayPointHandler onPointerEnter;

    private void Awake()
    {
        Connections = new Dictionary<int, (WayPoint point, Road road)>();
        RoadManager.AddPoint(this);

        _image = GetComponent<Image>();
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(gameObject.GetInstanceID());
    }

    public void PointerEnter()
    {
        onPointerEnter?.Invoke(gameObject.GetInstanceID());

        PathCreator.ProcessWayPoint(this);
    }

    public bool IsConnected(WayPoint other)
    {
        return Connections.ContainsKey(other.gameObject.GetInstanceID());
    }

    public WayPoint ExtrudePoint()
    {
        WayPoint point = Manager.BuildPoint(transform.position);
        Manager.BuildRoad(this, point);

        return point;
    }

    public void ConnectPoint(WayPoint point, Road road)
    {
        if (Connections.TryAdd(point.gameObject.GetInstanceID(), (point, road)))
            point.onDestroyed += DisconnectPoint;
    }

    public void DisconnectPoint(int id)
    {
        Connections.Remove(id);

        Debug.Log("Disconnected!");
    }

    public void DrawAsPathPart()
    {
        _image.color = Color.green;
    }

    public void ResetVisualization()
    {
        _image.color = Color.white;
    }
}