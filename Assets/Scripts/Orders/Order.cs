using System.Collections.Generic;

namespace OrdersManagement
{
    public class Order
    {
        public List<ProductType> neededProducts { get; private set; }
        public List<ProductType> ready { get; private set; }
        public List<ProductType> await { get; private set; }
        public List<ProductType> inHands { get; private set; }

        public event OrderEventHandler onOrderReady;
        public event OrderEventHandler onStatesChanged;

        public Order(List<ProductType> info)
        {
            neededProducts = new List<ProductType>(info);
            await = new List<ProductType>(info);
            inHands = new List<ProductType>();
            ready = new List<ProductType>();

            UpdateStates();
            OrderStateController.onTypesChanged += UpdateStates;
        }

        ~Order()
        {
            onOrderReady = null;
            onStatesChanged = null;
        }

        public void UpdateStates()
        {
            await = new List<ProductType>();
            inHands = new List<ProductType>();

            for (int i = neededProducts.Count - 1; i >= 0; i--) {
                var item = neededProducts[i];

                if (OrderStateController.Contains(item))
                    inHands.Add(item);
                else
                    await.Add(item);
            }

            onStatesChanged?.Invoke();
        }

        public bool TryAddItem(ProductType type)
        {
            //≈сли удалось удалить товар из одной из категорий
            if (!neededProducts.Remove(type))
                return false;

            ready.Add(type);

            if (!inHands.Remove(type))
                await.Remove(type);

            if (neededProducts.Count == 0)
                onOrderReady?.Invoke();

            return true;
        }
    }
}
