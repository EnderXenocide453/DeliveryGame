using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private bool AllowAllTypes;
    [SerializeField] private GoodsVisualizer GoodsVisualizer;

    /// <summary>
    /// Допустимые к хранению типы товара
    /// </summary>
    public List<ProductType> AllowedTypes;
    public int MaxCount = 5;        

    public bool Filled { get; private set; }
    public bool Empty { get; private set; } = true;

    private int _currentCount;
    private Dictionary<ProductType, int> _storedProducts;

    public Dictionary<ProductType, int> StoredProducts
    {
        get => new Dictionary<ProductType, int>(_storedProducts);   
    }

    public int CurrentCount
    {
        get => _currentCount;
        private set
        {
            _currentCount = value;

            if (value >= MaxCount)
                Filled = true;
            else if (value <= 0)
                Empty = true;
            else {
                Filled = Empty = false;
            }
        }
    }

    private void Start()
    {
        Init();
    }

    #region public methods

    public int AddProduct(ProductType type, int count)
    {
        GoodsVisualizer?.AddItem(type);

        return SetProductCount(type, GetProductCount(type) + count);
    }

    public int RemoveProduct(ProductType type, int count)
    {
        //Пока что удаляем объект со сцены
        Destroy(GoodsVisualizer?.RemoveItem(type).gameObject);

        return SetProductCount(type, GetProductCount(type) - count);
    }

    public int GetProductCount(ProductType type)
    {
        return _storedProducts.ContainsKey(type) ? _storedProducts[type] : 0;
    }

    public int SetProductCount(ProductType type, int count)
    {
        if (!AllowedTypes.Contains(type))
            return count;

        int allowedCount = Mathf.Clamp(count, 0, MaxCount);
        int delta = allowedCount - _storedProducts[type];

        _storedProducts[type] = allowedCount;
        CurrentCount += delta;

        return count - allowedCount;
    }

    public void GetAllGoodsFrom(Storage other)
    {
        foreach (var type in AllowedTypes)
            other.SetProductCount(type, AddProduct(type, other._storedProducts[type]));

        other.GoodsVisualizer?.VisualizeGoods(other._storedProducts);
    }

    #endregion public methods

    #region private methods

    private void Init()
    {
        _storedProducts = new Dictionary<ProductType, int>();

        if (AllowAllTypes)
            AllowedTypes = new List<ProductType>(GoodsManager.UsedProductTypes);

        foreach (var type in AllowedTypes)
            _storedProducts.TryAdd(type, 0);
    }

    #endregion private methods
}