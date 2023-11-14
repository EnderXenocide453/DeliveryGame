using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GoodsVisualizer : MonoBehaviour
{
    public abstract void AddItem(ProductType type);
    public abstract GoodsStackItem RemoveItem(ProductType type);
}
