using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer)), RequireComponent(typeof(GoodsVisualizer))]
public class MapCourier : MonoBehaviour
{
    public float Speed = 0.2f;
    public bool IsAwaits = true;

    public MapPath CourierPath;
    public Coroutine MoveCoroutine;

    private int _cash;

    private bool _isDrag;
    private Transform _courierHandler;

    private RectTransform _rectTransform;
    private Image _image;
    private LineRenderer _lastPointConnectionLine;
    
    private WayPoint _startPoint;

    private Courier _worldCourier;
    private GoodsIconsVisualizer _iconsVisualizer;

    public int Cash
    {
        get => _cash;

        set
        {
            _cash = value;
            //Вывод на экран
        }
    }

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();

        _image = GetComponent<Image>();
        _lastPointConnectionLine = GetComponent<LineRenderer>();
        _lastPointConnectionLine.enabled = false;

        _courierHandler = transform.parent;

        CourierPath.onDistanceChanged += UpdatePosition;
        CourierPath.onReachedPoint += CheckWayPoint;

        _iconsVisualizer = GetComponent<GoodsIconsVisualizer>();
        _iconsVisualizer.VisualizeGoods(_worldCourier.CourierStorage.StoredProducts);
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
        if (!IsAwaits)
            transform.position = CourierPath.GetCurrentPosition() - Vector3.forward * 0.01f;
    }

    public void SetWorldCourier(Courier courier)
    {
        _worldCourier = courier;
    }

    public void OnEndDelivery()
    {
        Debug.Log($"Earned: {Cash} cash");
        GlobalValueHandler.Cash += Cash;
        _worldCourier.OnReturn();
    }

    private void FollowCoursor()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);

        _rectTransform.position = _rectTransform.TransformPoint(position);
        _lastPointConnectionLine.SetPositions(new Vector3[] { transform.position, CourierPath.LastPoint.transform.position });
    }

    private void CheckWayPoint(WayPoint point)
    {
        if (point.pointOrder != null && point.pointOrder.CheckStorage(_worldCourier.CourierStorage)) {
            Cash += point.pointOrder.TakeOrderFromCourier(_worldCourier);
            _iconsVisualizer.Clear();
        }
    }

    //Вывод на экран иконок ресурсов
}
