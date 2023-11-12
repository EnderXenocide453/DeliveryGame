using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCourierManager : MonoBehaviour
{
    public static MapCourierManager instance;

    [SerializeField] private WayPoint StartPoint;
    [SerializeField] private GameObject MapCourierPrefab;
    [SerializeField] private List<MapCourier> Couriers;

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

        MapCourier courier = Instantiate(instance.MapCourierPrefab, instance.transform).GetComponent<MapCourier>();
        courier.SetStartPoint(instance.StartPoint);
        instance.Couriers.Add(courier);
    }
}
