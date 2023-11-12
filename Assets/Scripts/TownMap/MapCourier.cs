using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class MapCourier : MonoBehaviour
{
    [SerializeField] private float Speed = 0.2f;

    public MapPath CourierPath;
    public bool IsAwaits { get; private set; } = true;
    
    private bool _isDrag;
    private Transform _courierHandler;

    private RectTransform _rectTransform;
    private WayPoint _startPoint;
    private LineRenderer _lastPointConnectionLine;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _lastPointConnectionLine = GetComponent<LineRenderer>();
        _lastPointConnectionLine.enabled = false;

        _courierHandler = transform.parent;
    }

    private void Update()
    {
        if (_isDrag) {
            FollowCoursor();
        }
    }

    public void SetStartPoint(WayPoint point)
    {
        _startPoint = point;

        CourierPath = new MapPath();
        CourierPath.TryAddPoint(_startPoint);
    }

    public void StartDrag()
    {
        _isDrag = true;
        _lastPointConnectionLine.enabled = true;
        transform.SetParent(_courierHandler.parent);
    }

    public void StopDrag()
    {
        _isDrag = false;

        _lastPointConnectionLine.enabled = false;
        transform.SetParent(_courierHandler);
    }

    private void FollowCoursor()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);

        _rectTransform.position = _rectTransform.TransformPoint(position);
        _lastPointConnectionLine.SetPositions(new Vector3[] { transform.position, CourierPath.LastPoint.transform.position });
    }
}
