using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class MapCourier : MonoBehaviour
{
    public float Speed = 0.2f;
    public bool IsAwaits = true;

    public MapPath CourierPath;
    public Coroutine MoveCoroutine;

    private bool _isDrag;
    private Transform _courierHandler;

    private RectTransform _rectTransform;
    private Image _image;
    private LineRenderer _lastPointConnectionLine;
    
    private WayPoint _startPoint;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _image = GetComponent<Image>();
        _lastPointConnectionLine = GetComponent<LineRenderer>();
        _lastPointConnectionLine.enabled = false;

        _courierHandler = transform.parent;

        CourierPath.onDistanceChanged += UpdatePosition;
        CourierPath.onReachedPoint += (WayPoint point) => Debug.Log("Достигнута точка");
    }

    private void Update()
    {
        if (_isDrag) {
            FollowCoursor();
        }
    }

    private void OnEnable()
    {
        UpdatePosition();
    }

    public void SetStartPoint(WayPoint point)
    {
        _startPoint = point;

        CourierPath = new MapPath();
        CourierPath.TryAddPoint(_startPoint);
    }

    public void StartDrag()
    {
        if (!IsAwaits)
            return;

        _isDrag = true;

        _lastPointConnectionLine.enabled = true;
        transform.SetParent(_courierHandler.parent);
        _image.raycastTarget = false;

        PathCreator.SetActiveCourier(this);
    }

    public void StopDrag()
    {
        if (!IsAwaits)
            return;

        _isDrag = false;

        _lastPointConnectionLine.enabled = false;
        
        _image.raycastTarget = true;

        if (CourierPath.PathLength == 0) {
            transform.SetParent(_courierHandler);
            PathCreator.SetActiveCourier(null);
        }
        else {
            StartDelivery();
        }
    }

    public void StartDelivery()
    {
        MapCourierManager.StartDelivery(this);
    }

    public void UpdatePosition()
    {
        Debug.Log("a");
        if (!IsAwaits)
            transform.position = CourierPath.GetCurrentPosition() - Vector3.forward * 0.01f;
    }

    private void FollowCoursor()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);

        _rectTransform.position = _rectTransform.TransformPoint(position);
        _lastPointConnectionLine.SetPositions(new Vector3[] { transform.position, CourierPath.LastPoint.transform.position });
    }
}
