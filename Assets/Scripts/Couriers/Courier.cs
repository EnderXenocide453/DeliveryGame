using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Storage)), RequireComponent(typeof(Rigidbody))] 
public class Courier : MonoBehaviour
{
    public float worldSpeed = 10;
    public float rotationSpeed = 360;
    public float mapSpeed = 1;

    public float speedModifier = 1;

    public Storage CourierStorage { get; private set; }

    private bool _isMove;

    private Vector3 _moveDir;
    private Transform _target;

    private Rigidbody _body;

    public delegate void CourierEventHandler();
    public event CourierEventHandler onReturned;
    public event CourierEventHandler onOrderReceived;
    public CourierEventHandler onReachedTarget;

    private void Start()
    {
        CourierStorage = GetComponent<Storage>();
        _body = GetComponent<Rigidbody>();
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
        if (SearchForCorrectOrder(storage)) {
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

    private bool SearchForCorrectOrder(Storage storage)
    {
        foreach (var order in OrdersManager.instance.ActiveOrders) {
            if (order.GoodsCount != storage.CurrentCount || order.GoodsCount > CourierStorage.MaxCount)
                continue;

            if (order.CheckAllowedTypes(CourierStorage) && order.CheckStorage(storage))
                return true;
        }

        return false;
    }

    private void ApplyOrder()
    {
        Debug.Log("Apply");
        MapCourierManager.AddCourier(this);

        //Сообщаем о соответствии
        onOrderReceived?.Invoke();
    }

    private void DenyOrder()
    {
        Debug.Log("Deny");
        //Сообщаем о несоответствии
    }

    private void Move()
    {
        _moveDir = _target.position - transform.position;

        _moveDir = Vector3.ClampMagnitude(new Vector3(_moveDir.x, 0, _moveDir.z), 1);

        if (_moveDir.magnitude <= 0.1f) {
            if (_isMove) {
                onReachedTarget?.Invoke();
                _isMove = false;
            }
            return;
        }

        _isMove = true;

        Quaternion toRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);

        Vector3 move = new Vector3(_moveDir.x, 0, _moveDir.z);
        _body.velocity = move * worldSpeed * Time.fixedDeltaTime;
    }
}
