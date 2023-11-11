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

public class Path
{
    public float PathLength { get; private set; }

    private List<WayPoint> _wayPoints;
    private int _currentPoint = 0;

    public WayPoint CurrentPoint { get => _wayPoints[_currentPoint]; }
    public WayPoint NextPoint
    {
        get
        {
            if (_currentPoint + 1 == _wayPoints.Count)
                return null;

            return _wayPoints[_currentPoint + 1];
        }
    }

    public bool TryAddPoint(WayPoint point)
    {
        if (_wayPoints == null) {
            _wayPoints = new List<WayPoint>();
            _wayPoints.Add(point);

            return true;
        } 
        else if (_wayPoints[_wayPoints.Count - 1].Connections.ContainsKey(point.gameObject.GetInstanceID())) {
            PathLength += Vector3.Distance(_wayPoints[_wayPoints.Count - 1].transform.position, point.transform.position);

            _wayPoints.Add(point);

            return true;
        }

        return false;
    }

    public void ClearPath()
    {
        _wayPoints = null;
        _currentPoint = 0;
        PathLength = 0;
    }

    public Path GetReversedPath()
    {
        Path reversed = new Path();
        reversed.PathLength = PathLength;
        reversed._currentPoint = _wayPoints.Count - _currentPoint - 1;

        reversed._wayPoints = new List<WayPoint>();
        foreach (var point in _wayPoints)
            reversed._wayPoints.Insert(0, point);

        return reversed;
    }

    public bool MoveToNext()
    {
        if (!NextPoint)
            return false;

        _currentPoint++;

        return true;
    }
}