using System.Collections.Generic;

public class Order
{
    public Dictionary<ProductType, int> OrderInfo { get; private set; }
    public int GoodsCount { get; private set; }

    public delegate void OrderEventHandler();
    public event OrderEventHandler onFinished;

    public Order(Dictionary<ProductType, int> info)
    {
        OrderInfo = info;

        foreach (var count in OrderInfo.Values)
            GoodsCount += count;
    }

    public bool CheckStorage(Storage storage)
    {
        if (storage.CurrentCount != GoodsCount)
            return false;

        foreach (var type in OrderInfo.Keys) {
            if (!storage.AllowedTypes.Contains(type) || storage.GetProductCount(type) != OrderInfo[type])
                return false;
        }

        return true;
    }

    public int TakeOrderFromCourier(Courier courier)
    {
        if (!CheckStorage(courier.CourierStorage))
            return 0;

        int income = 0;

        foreach (var type in OrderInfo.Keys) {
            courier.CourierStorage.RemoveProduct(type, OrderInfo[type]);

            income += GoodsManager.GetProductInfo(type).Cost * OrderInfo[type];
        }

        onFinished?.Invoke();
        onFinished = null;

        OrderInfo = null;

        return income;
    }
}
