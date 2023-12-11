using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    public static bool isMaxCouriers;
    public static CourierManager instance;
    public static List<Courier> Couriers;
    public static List<Courier> CouriersInQueue;
    public static int MaxCount => instance.couriersMaxCount;

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
        CouriersInQueue = new List<Courier>();
    }

    public static void SetAwaitingCourier(Courier courier)
    {
        instance._awaitingCourier = courier;
    }

    public static bool SendGoodsToAwaiting(Storage storage)
    {
        if (!instance._awaitingCourier)
            return false;

        return instance._awaitingCourier.ReceiveOrderFromStorage(storage);
    }

    public Courier AddNewCourier()
    {
        Courier courier = Instantiate(instance.CourierPrefab, instance.Entrance.position, Quaternion.identity).GetComponent<Courier>();
        courier.ID = Couriers.Count;
        Couriers.Add(courier);
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

        CouriersInQueue.Add(courier);

        if (CouriersInQueue.Count <= instance.CourierQueue.QueuePoints.Length)
            courier.SetTarget(instance.CourierQueue.QueuePoints[CouriersInQueue.Count - 1]);
    }

    public void RemoveCourierFromQueue()
    {
        Courier courier = CouriersInQueue[0];

        courier.SetTarget(instance.Exit);
        courier.onReachedTarget = courier.Disappear;

        CouriersInQueue.RemoveAt(0);

        MoveQueue();
    }

    public void CreateQueue()
    {
        CourierQueue.CreateQueue();
    }

    private void MoveQueue()
    {
        for (int i = 0; i < CouriersInQueue.Count && i < instance.CourierQueue.QueuePoints.Length; i++) {
            CouriersInQueue[i].SetTarget(instance.CourierQueue.QueuePoints[i]);
        }
    }
}
