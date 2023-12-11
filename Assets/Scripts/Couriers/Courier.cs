using UnityEngine;

[RequireComponent(typeof(Storage)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(GoodsIconsVisualizer)), RequireComponent(typeof(EmojiCloud))] 
public class Courier : MonoBehaviour
{
    public int ID;

    public float worldSpeed = 10;
    public float rotationSpeed = 360;
    public float mapSpeedModifier = 1;

    public float speedModifier = 1;

    public bool isMove;
    public Storage CourierStorage { get; private set; }
    public CourierUpgradeQueue UpgradeQueue { get; private set; }

    private Vector3 _moveDir;
    private Transform _target;

    private Rigidbody _body;
    private Animator _animator;

    private GoodsIconsVisualizer _iconsVisualizer;
    private WayPoint _orderPoint;
    private EmojiCloud _cloud;

    public WayPoint CurrentOrderPoint
    {
        get => _orderPoint;
        set
        {
            _orderPoint = value;

            if (value != null) {
                _iconsVisualizer.VisualizeGoods(CurrentOrderPoint.pointOrder.OrderInfo);
            } else {
                _iconsVisualizer.Clear();
            }
        }
    }

    public delegate void CourierEventHandler();
    public event CourierEventHandler onReturned;
    public event CourierEventHandler onOrderReceived;
    public CourierEventHandler onReachedTarget;

    private void Awake()
    {
        CourierStorage = GetComponent<Storage>();
        UpgradeQueue = GetComponent<CourierUpgradeQueue>();
        UpgradeQueue.UpgradeQueue.onUpgraded += OnUpgraded;

        _body = GetComponent<Rigidbody>();
        _iconsVisualizer = GetComponent<GoodsIconsVisualizer>();
        _cloud = GetComponent<EmojiCloud>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if (_target)
            Move();
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }

    public bool ReceiveOrderFromStorage(Storage storage)
    {
        if (CurrentOrderPoint.pointOrder.CheckStorage(storage)) {
            CourierStorage.GetAllGoodsFrom(storage);
            ApplyOrder();
            return true;
        }

        DenyOrder();
        return false;
    }

    public void OnReturn()
    {
        onReturned?.Invoke();
    }

    private void ApplyOrder()
    {
        MapCourierManager.AddCourier(this);

        _cloud.DrawImage(GlobalValueHandler.ApplyIcon, 2f);
        SoundsManager.PlaySound(SoundsManager.instance.orderGoodsSound);
        Vibration.LongVibration(0.03f);

        onOrderReceived?.Invoke();
    }

    private void DenyOrder()
    {
        _cloud.DrawImage(GlobalValueHandler.DenyIcon, 2f);
    }

    private void Move()
    {
        _moveDir = _target.position - transform.position;

        _moveDir = Vector3.ClampMagnitude(new Vector3(_moveDir.x, 0, _moveDir.z), 1);

        if (_moveDir.magnitude <= 0.1f) {
            if (isMove) {
                onReachedTarget?.Invoke();
                isMove = false;

                _animator.SetBool("IsMove", false);
                _body.velocity = Vector3.zero;
            }
            return;
        }

        isMove = true;
        _animator.SetBool("IsMove", true);

        Quaternion toRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);

        Vector3 move = new Vector3(_moveDir.x, 0, _moveDir.z);
        _body.velocity = move * worldSpeed * Time.fixedDeltaTime;
    }

    private void OnUpgraded()
    {
        _animator = GetComponentInChildren<Animator>();
    }
}
