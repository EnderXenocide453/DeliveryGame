using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public static GoodsManager instance = null;
    public static List<ProductType> UsedProductTypes;

    [SerializeField] private Product[] productsInfo;
    [SerializeField] private float ProductDelay = 0.5f;

    private Dictionary<ProductType, Product> _generatedProducts;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);

        instance = this;

        Init();
    }

    #region public methods
    public static Coroutine TransportGoods(Storage from, Storage to)
    {
        List<ProductType> typeMatch = new List<ProductType>();
        foreach (var type in from.AllowedTypes) {
            if (to.AllowedTypes.Contains(type))
                typeMatch.Add(type);
        }

        if (typeMatch.Count == 0)
            return null;

        return instance.StartCoroutine(instance.TransportGoods(from, to, typeMatch, instance.ProductDelay));
    }

    public static Coroutine SpawnGoodsTo(Storage target, ProductType type)
    {
        if (!target.AllowedTypes.Contains(type))
            return null;

        return instance.StartCoroutine(instance.SpawnGoods(target, type, instance.ProductDelay));
    }

    public static Product GetProduct(ProductType type)
    {
        instance._generatedProducts.TryGetValue(type, out var product);
        product = product.Clone;

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
            if (_generatedProducts.TryAdd(product.Type, new Product(product.Type, product.Name, 0, product.Cost)))
                UsedProductTypes.Add(product.Type);
        }
    }

    #endregion private methods

    private IEnumerator TransportGoods(Storage from, Storage to, List<ProductType> types, float delay) 
    {
        foreach (var type in types) {

            while (from.GetProductCount(type) > 0 && !to.Filled) {
                from.RemoveProduct(type, 1);
                to.AddProduct(type, 1);

                yield return new WaitForSeconds(delay);
            }
        }
    }

    private IEnumerator SpawnGoods(Storage target, ProductType type, float delay)
    {
        while (!target.Filled) {
            target.AddProduct(type, 1);

            yield return new WaitForSeconds(delay);
        }
    }
}

