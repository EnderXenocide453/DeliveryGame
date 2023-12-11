using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsManager : MonoBehaviour
{
    public static GoodsManager instance = null;
    public static List<ProductType> UsedProductTypes;
    public float ProductDelay = 1f;

    private static Dictionary<ProductType, Product> _generatedProducts;

    public static int BoxCount
    {
        get => instance.boxCount;
        set => instance.boxCount = value;
    }

    [SerializeField] private Product[] productsInfo;
    [SerializeField] private int boxCount = 1;

    public int StartCash;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);

        instance = this;

        Init();

        GlobalValueHandler.Cash = StartCash;
    }

    #region public methods
    public static void StartTransportGoods(Storage from, Storage to, Timer timer)
    {
        if (to.Filled || from.Empty)
            return;

        List<ProductType> typeMatch = new List<ProductType>();
        foreach (var type in from.AllowedTypes) {
            if (!to.containBoxes && to.AllowedTypes.Contains(type) && from.GetProductCount(type) > 0)
                typeMatch.Add(type);
        }

        if (typeMatch.Count == 0)
            return;

        timer.StartTimer(instance.ProductDelay, repeat: true);
        timer.onTimeEnds += () => TransportGoods(from, to, typeMatch, timer);
    }

    public static void StartSpawnGoodsTo(Storage target, ProductType type, Timer timer)
    {
        if (!(target.Empty || target.containBoxes) || !target.AllowedTypes.Contains(type) || target.Filled)
            return;

        timer.StartTimer(instance.ProductDelay);
        timer.onTimeEnds += () => SpawnGoods(target, type, timer);
    }

    public static void StartConsumeGoods(Storage target, ProductType[] types, Timer timer)
    {
        if (target.Empty)
            return;

        List<ProductType> typeMatch = new List<ProductType>();
        foreach (var type in types) {
            if (target.GetProductCount(type) > 0)
                typeMatch.Add(type);
        }

        if (typeMatch.Count == 0)
            return;

        timer.StartTimer(instance.ProductDelay, repeat: false);
        timer.onTimeEnds += () => ConsumeGoods(target, typeMatch, timer);
    }

    public static Product GetProductInfo(ProductType type)
    {
        _generatedProducts.TryGetValue(type, out var product);

        return product;
    }

    public void AddMoney(int count) => GlobalValueHandler.Cash += count;

    public void AddMoneyFromAd(int count)
    {
        AdController.StartAd();
        AdController.onAdEnds = () => AddMoney(count);
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

    private static void TransportGoods(Storage from, Storage to, List<ProductType> types, Timer timer)
    {
        for (int i = types.Count - 1; i >= 0; i--) {
            var type = types[i];

            var info = _generatedProducts[type].GetContainedGoods();
            if (from.GetProductCount(type) <= 0 || to.AddProduct(info.type, info.count) == 0) {
                types.RemoveAt(i);
                continue;
            }

            from.RemoveProduct(type, 1);
            if (from.GetProductCount(type) <= 0)
                types.RemoveAt(i);

            SoundsManager.PlaySound(_generatedProducts[type].InteractSound);
            Vibration.SingleVibration();

            break;
        }

        if (to.Filled || from.Empty || types.Count == 0) {
            timer.StopTimer();
            return;
        }

        //timer.StartTimer(instance.ProductDelay, repeat: false);
    }

    private static void SpawnGoods(Storage target, ProductType type, Timer timer)
    {
        target.AddProduct(type, 1);

        SoundsManager.PlaySound(_generatedProducts[type].InteractSound);
        Vibration.SingleVibration();

        if (target.Filled) {
            timer.StopTimer();
            return;
        }
    }

    private static void ConsumeGoods(Storage target, List<ProductType> types, Timer timer)
    {
        foreach (var type in types) {
            target.SetProductCount(type, 0);
        }

        SoundsManager.PlaySound(_generatedProducts[ProductType.BurgerBox].InteractSound);
        Vibration.SingleVibration();

        timer.StopTimer();
    }
    #endregion private methods
}

