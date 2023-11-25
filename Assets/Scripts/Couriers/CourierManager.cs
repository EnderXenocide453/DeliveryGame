using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    public static bool isMaxCouriers;
    public static CourierManager instance;
    public static List<Courier> Couriers;

    private Courier _awaitingCourier;

    [SerializeField] int couriersMaxCount = 20;

    [SerializeField] Transform Entrance;
    [SerializeField] Transform Exit;

    [SerializeField] QueueController CourierQueue;

    [SerializeField] GameObject CourierPrefab;

    public delegate void CourierManagerEventsHandler(Courier target);
    public static event CourierManagerEventsHandler onCourierAdded;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else {
            Destroy(gameObject);
            return;
        }

        Couriers = new List<Courier>();
    }

    public static void SetAwaitingCourier(Courier courier)
    {
        instance._awaitingCourier = courier;
    }

    public static void SendGoodsToAwaiting(Storage storage)
    {
        instance._awaitingCourier?.ReceiveOrderFromStorage(storage);
    }

    public Courier AddNewCourier()
    {
        Courier courier = Instantiate(instance.CourierPrefab, instance.Entrance.position, Quaternion.identity).GetComponent<Courier>();
        AddCourierToQueue(courier);

        courier.onReturned += () => 
        { 
            if (courier.CourierStorage.Empty) {
                ShowCourier(courier);
                MapCourierManager.RemoveCourier(courier);
            }
        };
        courier.onOrderReceived += () =>
        {
            _awaitingCourier = null;
            RemoveCourierFromQueue();
            MapCourierManager.AddCourier(courier);
        };

        isMaxCouriers = Couriers.Count >= couriersMaxCount;

        onCourierAdded?.Invoke(courier);

        return courier;
    }

    public void ShowCourier(Courier courier)
    {
        courier.transform.position = Entrance.position;

        courier.Appear();
        AddCourierToQueue(courier);
    }

    public void AddCourierToQueue(Courier courier)
    {
        courier.onReachedTarget = null;

        Couriers.Add(courier);

        if (Couriers.Count <= instance.CourierQueue.QueuePoints.Length)
            courier.SetTarget(instance.CourierQueue.QueuePoints[Couriers.Count - 1]);
    }

    public void RemoveCourierFromQueue()
    {
        Courier courier = Couriers[0];

        courier.SetTarget(instance.Exit);
        courier.onReachedTarget = courier.Disappear;

        Couriers.RemoveAt(0);

        MoveQueue();
    }

    public void CreateQueue()
    {
        CourierQueue.CreateQueue();
    }

    private void MoveQueue()
    {
        for (int i = 0; i < Couriers.Count && i < instance.CourierQueue.QueuePoints.Length; i++) {
            Couriers[i].SetTarget(instance.CourierQueue.QueuePoints[i]);
        }
    }
}
