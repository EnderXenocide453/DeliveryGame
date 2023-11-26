using UnityEngine;

[System.Serializable]
public class Product
{
    public string Name;
    public ProductType Type;
    public int Cost;

    public bool IsContainer;
    //public int ContainedCount;
    public ProductType ContainedType;

    public GameObject Prefab;
    public Sprite Icon;
    public AudioClip InteractSound;

    public (ProductType type, int count) GetContainedGoods()
    {
        return IsContainer ? (ContainedType, GoodsManager.BoxCount) : (Type, 1);
    }
}

public enum ProductType
{
    Burger,
    Pizza,
    Sushi,
    Drink,
    Flower,
    BurgerBox,
    PizzaBox,
    SushiBox,
    DrinkBox,
    FlowerBox
}
