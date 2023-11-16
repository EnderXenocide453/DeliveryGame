using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourierManager : MonoBehaviour
{
    [SerializeField] private GameObject courierPrefab;
    [SerializeField] private float distanceBetweenCouriers = 1.5f;
    private List<GameObject> _couriers;
    [SerializeField] private bool spawnCourier;
    [SerializeField] private bool removeCourier;
    void Start()
    {
        _couriers = new List<GameObject>();
    }

    public void SpawnCourier()
    {
        GameObject newCourier = Instantiate(courierPrefab, transform.position, Quaternion.identity);
        _couriers.Add(newCourier);
        
        if (_couriers.Count > 1)
        {
            Vector3 previousCourierPosition = _couriers[_couriers.Count - 2].transform.position;
            newCourier.transform.position = previousCourierPosition - newCourier.transform.forward * distanceBetweenCouriers;
        }
    }
    public void RemoveCourier()
    {
        if (_couriers.Count > 0)
        {
            GameObject courierToRemove = _couriers[0];
            _couriers.RemoveAt(0);
            Destroy(courierToRemove);
        }
    }
}
