using System.Collections.Generic;

namespace OrdersManagement
{
    public class OrderInteraction
    {
        internal Order order { get; private set; }

        public event OrderEventHandler onOrderFinished;
        public event OrderEventHandler onOrderReady;

        public OrderInteraction(List<ProductType> info)
        {
            order = new Order(info);
            order.onOrderReady += onOrderReady;
        }

        ~OrderInteraction()
        {
            onOrderFinished = null;
            onOrderReady = null;
        }

        public bool GetItemsFromStorage(Storage storage)
        {
            bool success = false;

            foreach (var type in storage.StoredProducts.Keys) {
                while(storage.StoredProducts[type] > 0 && order.TryAddItem(type)) {
                    storage.RemoveProduct(type, 1);
                    success = true;
                }
            }

            return success;
        }

        public int TakeOrderFromCourier(Courier courier)
        {
            int income = 0;

            foreach (var type in order.ready)
                income += GoodsManager.GetProductInfo(type).Cost;

            onOrderFinished?.Invoke();
            onOrderFinished = null;

            return income;
        }
    }
}
