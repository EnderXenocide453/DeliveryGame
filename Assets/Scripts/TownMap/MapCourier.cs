using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class MapCourier : MonoBehaviour
{
    [SerializeField] private float Speed = 0.2f;
    [SerializeField, Range(0, 1)] private float PathProgress = 0;

    public MapPath CourierPath;
    public bool IsAwaits { get; private set; } = true;
    
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
    }

    private void Update()
    {
        if (_isDrag) {
            FollowCoursor();
        }
    }

    private void FixedUpdate()
    {
        if (!IsAwaits) {
            Move(Time.fixedDeltaTime);
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
        //transform.SetParent(_courierHandler);
        _image.raycastTarget = true;

        //PathCreator.SetActiveCourier(null);
        StartDelivery();
    }

    public void StartDelivery()
    {
        IsAwaits = false;
        CourierPath.onPathEnds += ComeBack;
    }

    public void ComeBack()
    {
        CourierPath = CourierPath.GetReversedPath();

        CourierPath.onPathEnds -= ComeBack;
        CourierPath.onPathEnds += EndDelivery;
    }

    private void EndDelivery()
    {
        IsAwaits = true;
        transform.parent = _courierHandler;

        CourierPath.onPathEnds -= EndDelivery;
    }

    private void Move(float deltaTime)
    {
        transform.position = CourierPath.MoveTowards(deltaTime * Speed) - Vector3.forward * 0.01f;
    }

    private void FollowCoursor()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);

        _rectTransform.position = _rectTransform.TransformPoint(position);
        _lastPointConnectionLine.SetPositions(new Vector3[] { transform.position, CourierPath.LastPoint.transform.position });
    }
}
