using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public static OrdersManager instance;
    public static Dictionary<int, Order> ActiveOrders;

    [SerializeField] float appearenceDelay = 30;
    [SerializeField] int goodsMaxCount = 1;
    [SerializeField] List<ProductType> goodsTypes;

    private List<WayPoint> _freePoints;

    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        _freePoints = new List<WayPoint>();
        foreach (var point in RoadManager.instance.WayPoints.Values) {
            if (point.isStartPoint)
                continue;

            _freePoints.Add(point);
        }

        ActiveOrders = new Dictionary<int, Order>();
    }

    public static void AddFreePoint(WayPoint point)
    {
        instance._freePoints.Add(point);
        ActiveOrders.Remove(point.GetInstanceID());
    }

    public static void ChangeMaxCount(int count)
    {
        instance.goodsMaxCount = count;
    }

    public static void AddProductType(ProductType type)
    {
        if (instance.goodsTypes.Contains(type))
            return;

        instance.goodsTypes.Add(type);
    }

    public static void GenerateOrderForCourier(Courier courier)
    {
        if (courier.CurrentOrderPoint) {
            Debug.LogWarning("Курьер уже имеет заказ");
            return;
        }

        if (instance._freePoints.Count == 0) {
            Debug.LogWarning("Не хватает свободных точек!");
            return;
        }

        int id = Random.Range(0, instance._freePoints.Count);

        WayPoint point = instance._freePoints[id];

        point.SetOrder(GenerateRandomOrder(Mathf.Min(instance.goodsMaxCount, courier.CourierStorage.MaxCount)));
        ActiveOrders.Add(point.GetInstanceID(), point.pointOrder);
        instance._freePoints.RemoveAt(id);

        courier.CurrentOrderPoint = point;
        SoundsManager.PlaySound(SoundsManager.instance.newOrderSound);
    }

    private static Order GenerateRandomOrder(int count)
    {
        Dictionary<ProductType, int> info = new Dictionary<ProductType, int>();

        for (int i = 0; i < count; i++) {
            ProductType type = instance.goodsTypes[Random.Range(0, instance.goodsTypes.Count)];

            if (!info.TryAdd(type, 1)) info[type]++;
        }

        return new Order(info);
    }
}
