using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class MapCourier : MonoBehaviour
{
    [SerializeField] private Image image;

    public float Speed = 0.2f;
    public bool IsAwaits = true;

    public MapPath CourierPath;
    public Coroutine MoveCoroutine;

    private int _cash;

    private bool _isDrag;
    private Transform _courierHandler;

    private RectTransform _rectTransform;
    private LineRenderer _lastPointConnectionLine;
    
    private WayPoint _startPoint;
    private Courier _courier;

    public int Cash
    {
        get => _cash;

        set
        {
            _cash = value;
            //Вывод на экран
        }
    }

    public Courier WorldCourier 
    { 
        get => _courier; 
        set
        {
            _courier = value;
            RedrawIcon();
        }
    }

    private void Awake()
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

    private void OnEnable()
    {
        UpdatePosition();

        RedrawIcon();
    }

    public void SetStartPoint(WayPoint point)
    {
        _startPoint = point;

        CourierPath = new MapPath();
        CourierPath.TryAddPoint(_startPoint);

        CourierPath.onDistanceChanged += UpdatePosition;
        CourierPath.onReachedPoint += CheckWayPoint;
    }

    public void StartDrag()
    {
        if (!IsAwaits)
            return;

        _isDrag = true;

        _lastPointConnectionLine.enabled = true;
        transform.SetParent(_courierHandler.parent);
        image.raycastTarget = false;

        PathCreator.SetActiveCourier(this);
    }

    public void StopDrag()
    {
        if (!IsAwaits)
            return;

        _isDrag = false;

        _lastPointConnectionLine.enabled = false;
        
        image.raycastTarget = true;

        if (!PathCreator.isCorrectPointExists) {
            transform.SetParent(_courierHandler);
            PathCreator.SetActiveCourier(null);
            CourierPath.ClearPath();
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
        if (gameObject.activeInHierarchy && !IsAwaits)
            transform.position = CourierPath.GetCurrentPosition() - Vector3.forward * 0.01f;
    }

    public void OnEndDelivery()
    {
        GlobalValueHandler.Cash += Cash;
        TutorialManager.instance.NextStep();
        WorldCourier.OnReturn();
    }

    private void FollowCoursor()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);

        _rectTransform.position = _rectTransform.TransformPoint(position);
        _lastPointConnectionLine.SetPositions(new Vector3[] { transform.position, CourierPath.LastPoint.transform.position });
    }

    private void CheckWayPoint(WayPoint point)
    {
        if (WorldCourier.CurrentOrderPoint != null && point.GetInstanceID() == WorldCourier.CurrentOrderPoint.GetInstanceID()) {
            Cash += point.orderInteraction.TakeOrderFromCourier(WorldCourier);

            WorldCourier.CurrentOrderPoint?.SetActivity(false);
            WorldCourier.CurrentOrderPoint = null;
        }
    }

    private void RedrawIcon()
    {
        if (!image)
            image = GetComponent<Image>();

        if (_courier)
            image.sprite = WorldCourier.UpgradeQueue.UpgradeQueue.CurrentWorldIcon;
    }

    //Вывод на экран иконок ресурсов
}
