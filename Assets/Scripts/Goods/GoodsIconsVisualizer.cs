using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsIconsVisualizer : MonoBehaviour
{
    [SerializeField] private Transform GoodsPanel;
    [SerializeField] private GameObject IconPrefab;

    public void VisualizeGoods(Dictionary<ProductType, int> goods)
    {
        Clear();

        if (goods == null)
            return;

        foreach (var type in goods.Keys) {
            var product = GoodsManager.GetProductInfo(type);

            var icon = Instantiate(IconPrefab, GoodsPanel).GetComponent<Image>();
            icon.sprite = product.Icon;
            icon.transform.SetParent(GoodsPanel);

            Debug.Log(GoodsPanel);
        }
    }

    public void Clear()
    {
        while (GoodsPanel.childCount > 0) {
            DestroyImmediate(GoodsPanel.GetChild(0).gameObject);
        }
    }
}
