using UnityEngine;

[RequireComponent(typeof(Rigidbody))] 
public class Courier : MonoBehaviour
{
    public int ID;

    #region World vars
    public float worldSpeed = 10;
    public float rotationSpeed = 360;
    public bool isMove;

    private Vector3 _moveDir;
    private Transform _target;

    private Rigidbody _body;
    private Animator _animator;
    #endregion
    #region Map vars
    public float mapSpeedModifier = 1;
    public int maxGoodsCount;

    private WayPoint _orderPoint;
    #endregion

    public CourierUpgradeQueue UpgradeQueue { get; private set; }

    public WayPoint CurrentOrderPoint
    {
        get => _orderPoint;
        set
        {
            _orderPoint = value;
            if (_orderPoint)
                _orderPoint.orderInteraction.onOrderReady += ApplyOrder;

            onOrderChanged?.Invoke();
        }
    }

    #region events
    public delegate void CourierEventHandler();
    public event CourierEventHandler onReturned;
    public event CourierEventHandler onOrderReceived;
    public event CourierEventHandler onOrderChanged;
    public CourierEventHandler onReachedTarget;
    #endregion

    private void Awake()
    {
        UpgradeQueue = GetComponent<CourierUpgradeQueue>();
        UpgradeQueue.UpgradeQueue.onUpgraded += OnUpgraded;

        _body = GetComponent<Rigidbody>();
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

    public void OnReturn()
    {
        onReturned?.Invoke();
    }

    private void ApplyOrder()
    {
        MapCourierManager.AddCourier(this);

        SoundsManager.PlaySound(SoundsManager.instance.orderGoodsSound);
        Vibration.SingleVibration();

        onOrderReceived?.Invoke();
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
