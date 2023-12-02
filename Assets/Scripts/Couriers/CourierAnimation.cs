using UnityEngine;
[RequireComponent(typeof(Courier))]
public class CourierAnimation : MonoBehaviour
{
    private Courier _courier;
    private Animator _courierAnim;
    private void Awake()
    {
        _courier = GetComponent<Courier>();
        _courierAnim = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _courierAnim.SetBool("IsMove", _courier.isMove);
    }
}
