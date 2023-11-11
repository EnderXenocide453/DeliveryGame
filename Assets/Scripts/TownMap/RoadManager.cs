using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;

    [SerializeField] private GameObject wayPointPrefab;
    [SerializeField] private GameObject roadPrefab;

    private Dictionary<int, WayPoint> _wayPoints;
    private Dictionary<int, Road> _roads;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        CalculateConnections();
    }

    #region Редактирование карты
    public WayPoint BuildPoint(Vector3 position)
    {
        WayPoint wayPoint = Instantiate(wayPointPrefab, position, Quaternion.identity, transform).GetComponent<WayPoint>();
        wayPoint.Manager = this;

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
    #endregion

    #region Инициализация карты
    public static void AddPoint(WayPoint wayPoint)
    {
        instance._wayPoints.TryAdd(wayPoint.gameObject.GetInstanceID(), wayPoint);

        wayPoint.onDestroyed += instance.DeletePoint;
    }

    public static void AddRoad(Road road)
    {
        instance._roads.TryAdd(road.gameObject.GetInstanceID(), road);

        road.onDestroyed += instance.DeleteRoad;
    }

    private void Init()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        _wayPoints = new Dictionary<int, WayPoint>();
        _roads = new Dictionary<int, Road>();

        Debug.Log("RoadManager initialized!");
    }

    private void CalculateConnections()
    {
        foreach (var road in _roads.Values) {
            WayPoint a = road.PointA, b = road.PointB;

            a.ConnectPoint(b);
            b.ConnectPoint(a);
        }
    }

    private void DeletePoint(int id)
    {
        _wayPoints.Remove(id);
    }

    private void DeleteRoad(int id)
    {
        _roads.Remove(id);
    }
    #endregion
}
