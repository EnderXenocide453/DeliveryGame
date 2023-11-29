using UnityEngine;
[RequireComponent(typeof(Courier))]
public class CourierAnimation : MonoBehaviour
{
    private Courier _courier;
    private Animator _courierAnime;
    private void Awake()
    {
        _courier = GetComponent<Courier>();
        _courierAnime = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        _courierAnime.SetBool("IsMove", _courier.isMove);
    }
}
