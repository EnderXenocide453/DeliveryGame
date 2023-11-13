using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    [SerializeField] private GameObject courierPrefab;
    private List<Transform> couriers;
    [SerializeField] private bool spawnCourier;
    void Start()
    {
        couriers = new List<Transform>();
    }

    void Update()
    {
        if (spawnCourier)
        {
            SpawnCourier();
            spawnCourier = false;
        }
    }

    void SpawnCourier()
    {
        Vector3 spawnAtPosition = new Vector3(Random.Range(-26, -34), 1, Random.Range(20, 6));
        Vector3 spawnPoint = spawnAtPosition;
        GameObject newCourier = Instantiate(courierPrefab, spawnPoint, Quaternion.identity);
        couriers.Add(newCourier.transform);
    }

    //Vector3 FindSpawnPoint()
    //{
    //    Vector3 randomPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));

    //    // ѕровер€ем, чтобы нова€ точка спауна не была слишком близко к другим курьерам
    //    // и находилась внутри ограниченной территории
    //    while (IsTooCloseToOtherCouriers(randomPoint) || !IsInsideSpawnArea(randomPoint))
    //    {
    //        randomPoint = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
    //    }

    //    return randomPoint;
    //}

    //bool IsTooCloseToOtherCouriers(Vector3 point)
    //{
    //    foreach (Transform courier in couriers)
    //    {
    //        if (Vector3.Distance(point, courier.position) < 2f) // ѕредположим, что минимальное рассто€ние между курьерами - 2 метра
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    //bool IsInsideSpawnArea(Vector3 point)
    //{
    //    return spawnArea.bounds.Contains(point);
    //}
}
