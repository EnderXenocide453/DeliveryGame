using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class Road : MonoBehaviour
{
    public WayPoint PointA, PointB;

    private LineRenderer _line;

    public delegate void RoadEventHandler(int id);
    public event RoadEventHandler onDestroyed;

    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        Draw();
    }

    private void Update()
    {
        if (!Application.isPlaying) {
            if (!PointA || !PointB) {
                DestroyImmediate(gameObject);
                return;
            }

            Draw();
        }
    }

    private void OnDestroy()
    {
        onDestroyed?.Invoke(gameObject.GetInstanceID()); ;
    }

    public void SetWayPoints(WayPoint a, WayPoint b)
    {
        PointA = a;
        PointB = b;
    }

    private void Init()
    {
        _line = GetComponent<LineRenderer>();

        if (RoadManager.instance)
            RoadManager.AddRoad(this);
    }

    private void Draw()
    {
        _line.SetPositions(new Vector3[] { PointA.transform.position, PointB.transform.position });
    }

    public void DrawAsPathPart()
    {
        _line.startColor = Color.green;
        _line.endColor = Color.green;
    }

    public void ResetVisualization()
    {
        _line.startColor = Color.white;
        _line.endColor = Color.white;
    }
}