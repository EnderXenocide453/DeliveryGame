using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GoodsIconsVisualizer))]
public class WayPoint : MonoBehaviour
{
    public bool isStartPoint;

    public Dictionary<int, (WayPoint point, Road road)> connections;
    public RoadManager Manager;

    public Order pointOrder { get; private set; } = null;

    private Image _image;
    private GoodsIconsVisualizer _iconsVisualizer;

    public delegate void WayPointHandler(int id);
    public event WayPointHandler onDestroyed;
    public event WayPointHandler onPointerEnter;

    private void Awake()
    {
        connections = new Dictionary<int, (WayPoint point, Road road)>();
        RoadManager.AddPoint(this);

        _image = GetComponent<Image>();
        _iconsVisualizer = GetComponent<GoodsIconsVisualizer>();

        if (isStartPoint && MapCourierManager.SetStartPoint(this)) 
            _image.color = Color.green;
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(gameObject.GetInstanceID());
        onDestroyed = null;
        onPointerEnter = null;
    }

    public void SetOrder(Order order)
    {
        pointOrder = order;
        order.onFinished += RemoveOrder;

        //Вызов метода отрисовки
        _iconsVisualizer.VisualizeGoods(order.OrderInfo);
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

    public WayPoint ExtrudePoint()
    {
        WayPoint point = Manager.BuildPoint(transform.position);
        Manager.BuildRoad(this, point);

        return point;
    }

    public void ConnectPoint(WayPoint point, Road road)
    {
        if (connections.TryAdd(point.gameObject.GetInstanceID(), (point, road)))
            point.onDestroyed += DisconnectPoint;
    }

    public void DisconnectPoint(int id)
    {
        connections.Remove(id);
    }

    private void RemoveOrder()
    {
        pointOrder = null;
        //Убираем иконки заказа
        _iconsVisualizer.Clear();

        OrdersManager.AddFreePoint(this);
    }
}