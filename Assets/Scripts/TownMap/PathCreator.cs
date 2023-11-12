using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathCreator : MonoBehaviour
{
    public static PathCreator instance;

    [SerializeField] private MapCourier ActiveCourier;

    private LineRenderer _pathLine;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        Input.multiTouchEnabled = false;
        _pathLine = GetComponent<LineRenderer>();
    }

    public static void ProcessWayPoint(WayPoint point)
    {
        if (!instance.ActiveCourier.CourierPath.LastPoint.IsConnected(point)) {
            return;
        }

        instance.ActiveCourier.CourierPath.TryAddPoint(point);
        
        DisplayActivePath();
    }

    public static void SetActiveCourier(MapCourier courier)
    {
        instance.ActiveCourier = courier;

        if (instance)
            DisplayActivePath();
        else
            HideActivePath();
    }

    public static void DisplayActivePath()
    {
        if (!instance.ActiveCourier) {
            HideActivePath();
            return;
        }

        instance._pathLine.positionCount = instance.ActiveCourier.CourierPath.pointsCount;
        for (int i = 0; i < instance._pathLine.positionCount; i++) {
            instance._pathLine.SetPosition(i, instance.ActiveCourier.CourierPath.GetWayPointAtIndex(i).transform.position);
        }

        instance._pathLine.enabled = true;
    }

    public static void HideActivePath()
    {
        instance._pathLine.enabled = false;
    }
}

public class MapPath
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
    public WayPoint LastPoint { get => _wayPoints[_wayPoints.Count - 1]; }

    public int pointsCount { get => _wayPoints.Count; }

    public bool TryAddPoint(WayPoint point)
    {
        if (_wayPoints == null) {
            _wayPoints = new List<WayPoint>();
            _wayPoints.Add(point);

            return true;
        } else if (_wayPoints[_wayPoints.Count - 1].IsConnected(point)) {
            PathLength += Vector3.Distance(_wayPoints[_wayPoints.Count - 1].transform.position, point.transform.position);

            _wayPoints.Add(point);

            return true;
        }

        return false;
    }

    public void ClearPath()
    {
        _wayPoints = new List<WayPoint>() { CurrentPoint };
        _currentPoint = 0;
        PathLength = 0;
    }

    public MapPath GetReversedPath()
    {
        MapPath reversed = new MapPath();
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

    public WayPoint GetWayPointAtIndex(int id)
    {
        if (id >= _wayPoints.Count) {
            Debug.LogWarning($"Путь не содержит точки с индексом {id}");
            return null;
        }

        return _wayPoints[id];
    }
}

public class MapPathVisualizer
{

}