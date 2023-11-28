using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    public static PathCreator instance;
    public static bool isCorrectPointExists { get; private set; }

    [SerializeField] private MapCourier ActiveCourier;
    [SerializeField] private LineRenderer pathLine;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        Input.multiTouchEnabled = false;

        if (!pathLine)
            pathLine = GetComponent<LineRenderer>();
    }

    private void OnDisable()
    {
        HideActivePath();
    }

    public static void ProcessWayPoint(WayPoint point)
    {
        if (isCorrectPointExists || !instance.ActiveCourier || !instance.ActiveCourier.IsAwaits || !instance.ActiveCourier.CourierPath.LastPoint.IsConnected(point)) {
            return;
        }

        instance.ActiveCourier.CourierPath.TryAddPoint(point);
        if (point.GetInstanceID() == instance.ActiveCourier.WorldCourier.CurrentOrderPoint.GetInstanceID())
            isCorrectPointExists = true;

        DisplayActivePath();
    }

    public static void SetActiveCourier(MapCourier courier)
    {
        if (instance.ActiveCourier)
            instance.ActiveCourier.WorldCourier.onReturned -= HideActivePath;

        if (instance.ActiveCourier)
            instance.ActiveCourier.WorldCourier.CurrentOrderPoint.SetActivity(false);

        instance.ActiveCourier = courier;
        isCorrectPointExists = false;

        if (courier) {
            DisplayActivePath();
            courier.WorldCourier.onReturned += HideActivePath;
            courier.WorldCourier.CurrentOrderPoint.SetActivity(true);
        } 
        else
            HideActivePath();
    }

    public static void DisplayActivePath()
    {
        if (!instance.ActiveCourier) {
            HideActivePath();
            return;
        }

        instance.pathLine.positionCount = instance.ActiveCourier.CourierPath.pointsCount;
        for (int i = 0; i < instance.pathLine.positionCount; i++) {
            instance.pathLine.SetPosition(i, instance.ActiveCourier.CourierPath.GetWayPointAtIndex(i).transform.position);
        }

        instance.pathLine.enabled = true;
    }

    public static void HideActivePath()
    {
        instance.ActiveCourier?.WorldCourier.CurrentOrderPoint?.SetActivity(false);

        instance.pathLine.positionCount = 0;
        instance.pathLine.enabled = false;
    }
}

public class MapPath
{
    public float PathLength { get; private set; }

    private List<WayPoint> _wayPoints;
    private List<float> _distances;
    private int _currentPoint = 0;
    private float _currentDistance = 0;

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
    public WayPoint LastPoint { get => _wayPoints[_wayPoints.Count - 1]; }

    public int pointsCount { get => _wayPoints.Count; }

    public delegate void PathPointEventHandler(WayPoint point);
    public event PathPointEventHandler onReachedPoint;

    public delegate void PathEventHandler();
    public event PathEventHandler onPathEnds;
    public event PathEventHandler onDistanceChanged;

    public bool TryAddPoint(WayPoint point)
    {
        if (_wayPoints == null) {
            _wayPoints = new List<WayPoint>();
            _wayPoints.Add(point);

            _distances = new List<float>();
            _distances.Add(0);

            return true;
        } else if (_wayPoints[_wayPoints.Count - 1].IsConnected(point)) {
            float delta = Vector3.Distance(_wayPoints[_wayPoints.Count - 1].transform.position, point.transform.position);
            PathLength += delta;

            _wayPoints.Add(point);
            _distances.Add(PathLength);

            return true;
        }

        return false;
    }

    public void ClearPath()
    {
        _wayPoints = new List<WayPoint>() { CurrentPoint };
        _distances = new List<float>() { 0 };

        _currentPoint = 0;
        _currentDistance = 0;
        PathLength = 0;

        onPathEnds = null;
    }

    public MapPath GetReversedPath()
    {
        MapPath reversed = new MapPath();

        for (int i = pointsCount - 1; i >= 0; i--) {
            reversed.TryAddPoint(_wayPoints[i]);
        }

        reversed.onDistanceChanged = onDistanceChanged;
        reversed.onReachedPoint = onReachedPoint;

        return reversed;
    }

    public WayPoint GetWayPointAtIndex(int id)
    {
        if (id >= _wayPoints.Count) {
            Debug.LogWarning($"Путь не содержит точки с индексом {id}");
            return null;
        }

        return _wayPoints[id];
    }

    public Vector3 GetCurrentPosition()
    {
        if (_currentDistance == PathLength)
            return LastPoint.transform.position;

        float delta = (_currentDistance - _distances[_currentPoint]) / (_distances[_currentPoint + 1] - _distances[_currentPoint]);
        return Vector3.Lerp(CurrentPoint.transform.position, NextPoint.transform.position, delta);
    }

    public Vector3 MoveTowards(float distance)
    {
        float newDistance = Mathf.Clamp(_currentDistance + distance, 0, PathLength);

        for (int i = _currentPoint; i < _wayPoints.Count; i++) {
            if (_distances[i] < _currentDistance)
                continue;

            if (_distances[i] > newDistance)
                break;

            _currentPoint = i;
            onReachedPoint?.Invoke(CurrentPoint);
        }

        _currentDistance = newDistance;
        onDistanceChanged?.Invoke();

        if (_currentDistance == PathLength) {
            onPathEnds?.Invoke();
            return LastPoint.transform.position;
        }

        float delta = (_currentDistance - _distances[_currentPoint]) / (_distances[_currentPoint + 1] - _distances[_currentPoint]);

        return Vector3.Lerp(CurrentPoint.transform.position, NextPoint.transform.position, delta);
    }
}