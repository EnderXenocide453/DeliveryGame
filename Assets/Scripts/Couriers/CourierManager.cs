using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    public static CourierManager instance;
    public List<Courier> Couriers;

    private Courier _awaitingCourier;

    [SerializeField] Transform Entrance;
    [SerializeField] Transform Exit;

    [SerializeField] QueueController CourierQueue;

    [SerializeField] GameObject CourierPrefab;

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

    public void AddNewCourier()
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
    }

    public void ShowCourier(Courier courier)
    {
        courier.Appear();
        AddCourierToQueue(courier);
    }

    public void AddCourierToQueue(Courier courier)
    {
        Couriers.Add(courier);

        if (Couriers.Count <= instance.CourierQueue.QueuePoints.Length)
            courier.SetTarget(instance.CourierQueue.QueuePoints[Couriers.Count - 1]);
    }

    public void RemoveCourierFromQueue()
    {
        Courier courier = Couriers[0];

        courier.SetTarget(instance.Exit);
        courier.onReachedTarget = () =>
        {
            Debug.Log("aboba");
            courier.Disappear();
            courier.onReachedTarget = null;
        };

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
