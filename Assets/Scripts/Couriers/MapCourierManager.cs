using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCourierManager : MonoBehaviour
{
    public static MapCourierManager instance;

    [SerializeField] float MoveDelay = 0.1f;
    [SerializeField] private WayPoint StartPoint;
    [SerializeField] private GameObject MapCourierPrefab;
    [SerializeField] private List<MapCourier> Couriers;

    public Transform CourierContainer;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        if (!StartPoint) {
            Debug.LogWarning("Не назначена начальная точка!");
            return;
        }

        AddCourier();
        AddCourier();
        AddCourier();
    }

    public static void AddCourier()
    {
        if (instance.Couriers == null) {
            instance.Couriers = new List<MapCourier>();
        }

        MapCourier courier = Instantiate(instance.MapCourierPrefab, instance.CourierContainer).GetComponent<MapCourier>();
        courier.SetStartPoint(instance.StartPoint);
        instance.Couriers.Add(courier);
    }

    public static void StartDelivery(MapCourier courier)
    {
        courier.IsAwaits = false;
        courier.MoveCoroutine = instance.StartCoroutine(MoveCourier(courier));

        courier.CourierPath.onPathEnds += () => ComeBack(courier);
    }

    public static void ComeBack(MapCourier courier)
    {
        courier.CourierPath = courier.CourierPath.GetReversedPath();

        courier.CourierPath.onPathEnds += () => EndDelivery(courier);
    }

    public static void EndDelivery(MapCourier courier)
    {
        courier.IsAwaits = true;
        instance.StopCoroutine(courier.MoveCoroutine);

        courier.CourierPath.ClearPath();
        courier.transform.SetParent(instance.CourierContainer);
    }

    private static IEnumerator MoveCourier(MapCourier courier)
    {
        while (true) {
            yield return new WaitForSeconds(instance.MoveDelay);
            courier.CourierPath.MoveTowards(instance.MoveDelay * courier.Speed);
        }
    }
}
