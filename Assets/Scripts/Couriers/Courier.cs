using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Courier : MonoBehaviour
{
    [SerializeField] private Order _currentOrder;

    public void TakeOrder(Order order)
    {
        _currentOrder = order;
        StartCoroutine(DeliverOrder());
    }
    private IEnumerator DeliverOrder()
    {
        Debug.Log("Coroutine!");
        transform.localScale = Vector3.zero;
        yield return new WaitForSeconds(_currentOrder.timeToDelivery);
        transform.localScale = Vector3.one; //new Vector3(-26, 1, 16);
        Debug.Log($"Доставлен заказ: {_currentOrder.quantity} {_currentOrder.productType}");

        _currentOrder = null;
    }
}
