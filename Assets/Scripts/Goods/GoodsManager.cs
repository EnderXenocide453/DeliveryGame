using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public static GoodsManager instance = null;
    public static List<ProductType> UsedProductTypes;
    public float ProductDelay = 1f;

    public static int BoxCount
    {
        get => instance.boxCount;
        set => instance.boxCount = value;
    }

    [SerializeField] private Product[] productsInfo;
    [SerializeField] private int boxCount = 1;

    public int StartCash;

    private Dictionary<ProductType, Product> _generatedProducts;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);

        instance = this;

        Init();

        GlobalValueHandler.Cash = StartCash;
    }

    #region public methods
    public static Coroutine TransportGoods(Storage from, Storage to, Timer timer)
    {
        List<ProductType> typeMatch = new List<ProductType>();
        foreach (var type in from.AllowedTypes) {
            if (to.AllowedTypes.Contains(type))
                typeMatch.Add(type);
        }

        if (typeMatch.Count == 0)
            return null;

        return instance.StartCoroutine(instance.TransportGoods(from, to, typeMatch, instance.ProductDelay, timer));
    }

    public static Coroutine SpawnGoodsTo(Storage target, ProductType type, Timer timer)
    {
        if (!target.AllowedTypes.Contains(type))
            return null;

        return instance.StartCoroutine(instance.SpawnGoods(target, type, instance.ProductDelay, timer));
    }

    public static Product GetProductInfo(ProductType type)
    {
        instance._generatedProducts.TryGetValue(type, out var product);

        return product;
    }

    #endregion public methods

    #region private methods

    private void Init()
    {
        _generatedProducts = new Dictionary<ProductType, Product>();
        UsedProductTypes = new List<ProductType>();

        if (productsInfo.Length == 0)
            return;

        foreach (var product in productsInfo) {
            if (_generatedProducts.TryAdd(product.Type, product))
                UsedProductTypes.Add(product.Type);
        }
    }

    #endregion private methods

    private IEnumerator TransportGoods(Storage from, Storage to, List<ProductType> types, float delay, Timer timer) 
    {
        foreach (var type in types) {

            while (from.GetProductCount(type) > 0 && !to.Filled) {
                timer?.StartTimer(delay);
                yield return new WaitForSeconds(delay);

                var info = _generatedProducts[type].GetContainedGoods();
                if (to.AddProduct(info.type, info.count) == 0)
                    break;

                from.RemoveProduct(type, 1);
                SoundsManager.PlaySound(_generatedProducts[type].InteractSound);
            }
        }
    }

    private IEnumerator SpawnGoods(Storage target, ProductType type, float delay, Timer timer)
    {
        while (!target.Filled) {
            timer?.StartTimer(delay);
            yield return new WaitForSeconds(delay);
            target.AddProduct(type, 1);

            SoundsManager.PlaySound(_generatedProducts[type].InteractSound);
        }
    }
}

