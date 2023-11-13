
[System.Serializable]
public class Order
{
    public string productType;
    public int quantity;
    public float timeToDelivery;

    public Order(string productType, int quantity, float timeToDelivery)
    {
        this.productType = productType;
        this.quantity = quantity;
        this.timeToDelivery = timeToDelivery;
    }
}