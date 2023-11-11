using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject wayPointPrefab;
    [SerializeField] private GameObject roadPrefab;

    private Dictionary<int, WayPoint> _wayPoints;

    private void Awake()
    {
        Init();
    }

    public WayPoint BuildPoint(Vector3 position)
    {
        WayPoint wayPoint = Instantiate(wayPointPrefab, position, Quaternion.identity, transform).GetComponent<WayPoint>();
        wayPoint.Manager = this;

        wayPoint.onDestroyed += DeletePoint;

        return wayPoint;
    }

    public void BuildRoad(WayPoint a, WayPoint b)
    {
        Road road = Instantiate(roadPrefab, transform).GetComponent<Road>();
        road.SetWayPoints(a, b);
    }

    public void BuildRoadBetweenSelected()
    {
        if (Selection.count != 2) {
            Debug.LogWarning("Можно объединить только 2 разные точки!");
            return;
        }

        WayPoint a = Selection.transforms[0].GetComponent<WayPoint>();
        WayPoint b = Selection.transforms[1].GetComponent<WayPoint>();

        BuildRoad(a, b);
    }

    private void Init()
    {
        if (_wayPoints == null)
            _wayPoints = new Dictionary<int, WayPoint>();

        Debug.Log("RoadManager initialized!");
    }

    private void DeletePoint(int id)
    {
        _wayPoints.Remove(id);
    }
}
