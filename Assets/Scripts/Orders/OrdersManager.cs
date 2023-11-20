using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersManager : MonoBehaviour
{
    public static OrdersManager instance;

    [SerializeField] float appearenceDelay = 30;
    [SerializeField] int goodsMaxCount = 1;
    [SerializeField] List<ProductType> goodsTypes;

    public List<Order> ActiveOrders;

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

        ActiveOrders = new List<Order>();
        StartCoroutine(OrderGenerator());
    }

    public static void AddFreePoint(WayPoint point)
    {
        instance._freePoints.Add(point);
    }

    public static void ChangeMaxCount(int count)
    {
        if (count > instance.goodsMaxCount)
            instance.goodsMaxCount = count;
    }

    private static void GenerateNewOrder()
    {
        if (instance._freePoints.Count == 0)
            return;

        int id = Random.Range(0, instance._freePoints.Count);

        Debug.Log($"id:{id} count:{instance._freePoints.Count}");

        WayPoint point = instance._freePoints[id];

        Dictionary<ProductType, int> info = new Dictionary<ProductType, int>();
        int count = Random.Range(1, instance.goodsMaxCount + 1);

        for (int i = 0; i < count; i++) {
            ProductType type = instance.goodsTypes[Random.Range(0, instance.goodsTypes.Count)];

            if (!info.TryAdd(type, 1)) info[type]++;
        }

        point.SetOrder(new Order(info));
        instance.ActiveOrders.Add(point.pointOrder);
        instance._freePoints.RemoveAt(id);
    }

    private IEnumerator OrderGenerator()
    {
        while (true) {
            yield return new WaitForSeconds(appearenceDelay);

            GenerateNewOrder();
        }
    }
}
