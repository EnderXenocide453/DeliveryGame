using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{
    [SerializeField] private bool AllowAllTypes;
    [SerializeField] private GoodsVisualizer GoodsVisualizer;
    [SerializeField] private int maxCount = 1;

    /// <summary>
    /// Допустимые к хранению типы товара
    /// </summary>
    public List<ProductType> AllowedTypes;
    public int MaxCount
    {
        get => maxCount;
        set
        {
            maxCount = value;
            Filled = _currentCount >= maxCount;
        }
    }

    public bool Filled { get; private set; }
    public bool Empty { get; private set; } = true;

    [SerializeField] private int _currentCount;
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

            Filled = _currentCount >= MaxCount;
            Empty = _currentCount <= 0;
        }
    }

    private void Start()
    {
        Init();
    }

    #region public methods

    public int AddProduct(ProductType type, int count)
    {
        return SetProductCount(type, GetProductCount(type) + count);
    }

    public int RemoveProduct(ProductType type, int count)
    {
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

        GoodsVisualizer?.VisualizeGoods(_storedProducts);

        return count - allowedCount;
    }

    public void GetAllGoodsFrom(Storage other)
    {
        foreach (var type in AllowedTypes)
            other.SetProductCount(type, AddProduct(type, other._storedProducts[type]));
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