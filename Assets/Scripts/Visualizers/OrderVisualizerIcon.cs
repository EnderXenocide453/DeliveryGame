using UnityEngine;
using UnityEngine.UI;

namespace OrdersManagement
{
    public class OrderVisualizerIcon : MonoBehaviour
    {
        [SerializeField] Image mainIcon;
        [SerializeField] Image background;
        [Header("Styles")]
        [SerializeField] OrderIconStyle await;
        [SerializeField] OrderIconStyle inHands;
        [SerializeField] OrderIconStyle ready;

        public void SetOrderItem(ProductType type, OrderItemState state)
        {
            var product = GoodsManager.GetProductInfo(type);

            mainIcon.sprite = product.Icon;

            switch (state) {
                case OrderItemState.Await:
                    ApplyStyle(await);
                    break;
                case OrderItemState.InHands:
                    ApplyStyle(inHands);
                    break;
                case OrderItemState.Ready:
                    ApplyStyle(ready);
                    break;
            }
        }

        private void ApplyStyle(OrderIconStyle style)
        {
            mainIcon.color = style.mainColor;

            background.enabled = style.backgroundSprite != null;
            background.sprite = style.backgroundSprite;
        }

        [System.Serializable]
        public struct OrderIconStyle
        {
            public Color mainColor;
            public Sprite backgroundSprite;
        }
    }

    public enum OrderItemState
    {
        Await,
        InHands,
        Ready
    }
}

