using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsStack : GoodsVisualizer
{
    private Dictionary<ProductType, List<GoodsStackItem>> _sortedStackItems;
    private List<GoodsStackItem> _itemList;

    private void Start()
    {
        _sortedStackItems = new Dictionary<ProductType, List<GoodsStackItem>>();
        _itemList = new List<GoodsStackItem>();

        foreach(var type in GoodsManager.UsedProductTypes)
            _sortedStackItems.Add(type, new List<GoodsStackItem>());
    }

    public override void AddItem(ProductType type)
    {
        Product product = GoodsManager.GetProductInfo(type);

        if (product == null)
            return;

        GoodsStackItem item = Instantiate(product.Prefab, transform).GetComponent<GoodsStackItem>();

        _sortedStackItems[type].Add(item);

        if (_itemList.Count == 0) {
            item.ConnectTo(null);
        }
        else {
            item.ConnectTo(_itemList[_itemList.Count - 1]);
        }

        _itemList.Add(item);
    }

    public override GoodsStackItem RemoveItem(ProductType type)
    {
        if (_sortedStackItems.TryGetValue(type, out var list)) {
            if (list.Count == 0)
                return null;

            var item = list[0];
            
            list.RemoveAt(0);
            Debug.Log(item.ID);
            _itemList.RemoveAt(item.ID);

            item.Disconnect();

            return item;
        }

        return null;
    }
}
