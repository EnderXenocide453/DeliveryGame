using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoodsIconsVisualizer : MonoBehaviour
{
    [SerializeField] private Transform GoodsPanel;
    [SerializeField] private GameObject IconPrefab;

    private void Awake()
    {
        //GoodsPanel.gameObject.SetActive(false);
    }

    public void VisualizeGoods(Dictionary<ProductType, int> goods)
    {
        Clear();

        if (goods == null)
            return;

        foreach (var type in goods.Keys) {
            for (int i = 0; i < goods[type]; i++) {
                AddIcon(type);
            }
        }

        GoodsPanel.gameObject.SetActive(true);
    }

    public void VisualizeGoods(List<ProductType> goods)
    {
        Clear();

        if (goods == null)
            return;

        foreach (var type in goods) {
            AddIcon(type);
        }

        GoodsPanel.gameObject.SetActive(true);
    }

    private void AddIcon(ProductType type)
    {
        var product = GoodsManager.GetProductInfo(type);

        var icon = Instantiate(IconPrefab, GoodsPanel).GetComponent<Image>();
        icon.sprite = product.Icon;
        icon.transform.SetParent(GoodsPanel);
    }

    public void Clear()
    {
        while (GoodsPanel.childCount > 0) {
            DestroyImmediate(GoodsPanel.GetChild(0).gameObject);
        }

        GoodsPanel.gameObject.SetActive(false);
    }
}
