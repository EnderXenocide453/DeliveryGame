using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public static RoadManager instance;

    [SerializeField] private GameObject wayPointPrefab;
    [SerializeField] private GameObject roadPrefab;

    public Dictionary<int, WayPoint> WayPoints;
    public Dictionary<int, Road> Roads;

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
        instance.WayPoints.TryAdd(wayPoint.gameObject.GetInstanceID(), wayPoint);

        wayPoint.onDestroyed += instance.DeletePoint;
    }

    public static void AddRoad(Road road)
    {
        instance.Roads.TryAdd(road.gameObject.GetInstanceID(), road);

        road.onDestroyed += instance.DeleteRoad;
    }

    private void Init()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        WayPoints = new Dictionary<int, WayPoint>();
        Roads = new Dictionary<int, Road>();
    }

    private void CalculateConnections()
    {
        foreach (var road in Roads.Values) {
            WayPoint a = road.PointA, b = road.PointB;

            a.ConnectPoint(b, road);
            b.ConnectPoint(a, road);
        }
    }

    private void DeletePoint(int id)
    {
        WayPoints.Remove(id);
    }

    private void DeleteRoad(int id)
    {
        Roads.Remove(id);
    }
    #endregion
}