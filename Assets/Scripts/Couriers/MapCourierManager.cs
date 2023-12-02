using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCourierManager : MonoBehaviour
{
    public static MapCourierManager instance;
    private static int activeCouriersCount;

    [SerializeField] float moveDelay = 0.1f;
    [SerializeField] int maxCount = 5;
    [SerializeField] private GameObject MapCourierPrefab;
    [SerializeField] private Dictionary<int, MapCourier> Couriers;

    public Transform CourierContainer;
    
    public WayPoint StartPoint { get; private set; }

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
    }

    public static void AddCourier(Courier courier)
    {
        if (instance.Couriers == null) {
            instance.Couriers = new Dictionary<int, MapCourier>();
        }

        if (instance.Couriers.ContainsKey(courier.GetInstanceID()))
            return;

        MapCourier mapCourier = Instantiate(instance.MapCourierPrefab, instance.CourierContainer).GetComponent<MapCourier>();
        mapCourier.SetStartPoint(instance.StartPoint);
        mapCourier.WorldCourier = courier;
        instance.Couriers.TryAdd(courier.GetInstanceID(), mapCourier);

        if (instance.CourierContainer.childCount > instance.maxCount) {
            mapCourier.gameObject.SetActive(false);
            return;
        }
    }

    public static void RemoveCourier(Courier courier)
    {
        if (instance.Couriers == null)
            return;

        int id = courier.GetInstanceID();

        Destroy(instance.Couriers[id].gameObject);
        instance.Couriers.Remove(id);
    }

    public static void StartDelivery(MapCourier courier)
    {
        courier.IsAwaits = false;
        courier.MoveCoroutine = instance.StartCoroutine(MoveCourier(courier));

        courier.CourierPath.onPathEnds += () => ComeBack(courier);

        if (instance.CourierContainer.childCount >= instance.maxCount)
            instance.CourierContainer.GetChild(instance.maxCount - 1).gameObject.SetActive(true);
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

        courier.OnEndDelivery();
    }

    public static bool SetStartPoint(WayPoint point)
    {
        if (instance.StartPoint)
            return false;

        instance.StartPoint = point;
        return true;
    }

    private static IEnumerator MoveCourier(MapCourier courier)
    {
        while (true) {
            yield return new WaitForSeconds(instance.moveDelay);
            courier.CourierPath.MoveTowards(instance.moveDelay * courier.Speed * courier.WorldCourier.mapSpeedModifier);
        }
    }
}
