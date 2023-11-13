using UnityEngine;

public class Stall : MonoBehaviour
{
    [SerializeField] private Courier courier;

    public void RequestOrder(string productType, int quantity, float deliveryTime)
    {
        Order order = new Order(productType, quantity, deliveryTime);

        courier.TakeOrder(order);
    }
}
