using UnityEngine;

namespace OrdersManagement
{
    [RequireComponent(typeof(Courier))]
    public class OrderVisualizer : MonoBehaviour
    {
        [SerializeField] Transform visualizerPanel;
        [SerializeField] GameObject iconPrefab;

        private Courier _courier;
        private Order _currentOrder;

        private void Start()
        {
            _courier = GetComponent<Courier>();
            _courier.onOrderChanged += VisualizeOrder;
        }

        public void VisualizeOrder()
        {
            if (!_courier.CurrentOrderPoint) {
                Clear();
                return;
            }

            _currentOrder = _courier.CurrentOrderPoint.orderInteraction.order;

            UpdateIcons();
            _currentOrder.onStatesChanged += UpdateIcons;
        }

        public void Clear()
        {
            for (int i = visualizerPanel.childCount - 1; i >= 0; i--) {
                Destroy(visualizerPanel.GetChild(i).gameObject);
            }
        }

        private void UpdateIcons()
        {
            Clear();

            foreach (var item in _currentOrder.inHands)
                AddIcon(item, OrderItemState.InHands);
            foreach (var item in _currentOrder.await)
                AddIcon(item, OrderItemState.Await);
            foreach (var item in _currentOrder.ready)
                AddIcon(item, OrderItemState.Ready);
        }

        private void AddIcon(ProductType type, OrderItemState state)
        {
            var icon = Instantiate(iconPrefab, visualizerPanel).GetComponent<OrderVisualizerIcon>();
            icon.SetOrderItem(type, state);
        }
    }
}

