using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodsVisualizer : MonoBehaviour
{
    public abstract void AddItem(ProductType type);
    public abstract GoodsStackItem RemoveItem(ProductType type);
    public abstract void Clear();
    public abstract void VisualizeGoods(Dictionary<ProductType, int> goods);
}
