using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class Road : MonoBehaviour
{
    public WayPoint PointA, PointB;

    private LineRenderer _line;

    public delegate void RoadEventHandler();
    public event RoadEventHandler onDestroyed;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _line?.SetPositions(new Vector3[] { PointA.transform.position, PointB.transform.position });
    }
    
    private void OnDestroy()
    {
        onDestroyed?.Invoke();
    }

    public void SetWayPoints(WayPoint a, WayPoint b)
    {
        PointA = a;
        PointB = b;

        a.onDestroyed += (int id) => { DestroyImmediate(gameObject); };
        b.onDestroyed += (int id) => { DestroyImmediate(gameObject); };
    }
}