using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OrdersManagement;

[RequireComponent(typeof(GoodsIconsVisualizer))]
public class WayPoint : MonoBehaviour
{
    public bool isStartPoint;

    public OrderInteraction orderInteraction { get; private set; }
    public Dictionary<int, (WayPoint point, Road road)> connections;
    public RoadManager Manager;

    private Image _image;

    public delegate void WayPointHandler(int id);
    public event WayPointHandler onDestroyed;
    public event WayPointHandler onPointerEnter;

    private void Awake()
    {
        connections = new Dictionary<int, (WayPoint point, Road road)>();
        RoadManager.AddPoint(this);

        _image = GetComponent<Image>();

        if (isStartPoint && MapCourierManager.SetStartPoint(this)) 
            _image.color = Color.red;
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(gameObject.GetInstanceID());
        onDestroyed = null;
        onPointerEnter = null;
    }

    public void SetOrder(OrderInteraction order)
    {
        orderInteraction = order;

        orderInteraction.onOrderFinished += RemoveOrder;
    }

    public void PointerEnter()
    {
        onPointerEnter?.Invoke(gameObject.GetInstanceID());

        PathCreator.ProcessWayPoint(this);
    }

    public bool IsConnected(WayPoint other)
    {
        return connections.ContainsKey(other.gameObject.GetInstanceID());
    }

#if UNITY_EDITOR
    public WayPoint ExtrudePoint()
    {
        WayPoint point = Manager.BuildPoint(transform.position);
        Manager.BuildRoad(this, point);

        return point;
    }
#endif

    public void ConnectPoint(WayPoint point, Road road)
    {
        if (connections.TryAdd(point.gameObject.GetInstanceID(), (point, road)))
            point.onDestroyed += DisconnectPoint;
    }

    public void DisconnectPoint(int id)
    {
        connections.Remove(id);
    }

    public void SetActivity(bool activity)
    {
        _image.color = activity ? Color.green : Color.white;
    }

    private void RemoveOrder()
    {
        orderInteraction = null;

        OrdersManager.AddFreePoint(this);
    }
}