using System.Collections.Generic;

namespace OrdersManagement
{
    public delegate void OrderEventHandler();

    public static class OrderStateController
    {
        private static List<ProductType> _allowedTypes;
        private static Storage _targetStorage;

        public delegate void OrderEventHandler();
        public static event OrderEventHandler onTypesChanged;

        /// <summary>
        /// ”станавливает отслеживаемое хранилище дл€ вычислени€ состо€ни€ заказов
        /// </summary>
        /// <param name="target">÷елевое хранилище</param>
        public static void SetTargetStorage(Storage target)
        {
            if (_targetStorage)
                _targetStorage.onCountChanged -= UpdateAllowedTypes;

            _targetStorage = target;
            _targetStorage.onCountChanged += UpdateAllowedTypes;
            UpdateAllowedTypes();
        }

        public static bool Contains(ProductType type) => _allowedTypes.Contains(type);

        private static void UpdateAllowedTypes()
        {
            _allowedTypes = new List<ProductType>();

            foreach (var product in _targetStorage.StoredProducts) {
                if (product.Value > 0)
                    _allowedTypes.Add(product.Key);
            }

            onTypesChanged?.Invoke();
        }
    }
}

