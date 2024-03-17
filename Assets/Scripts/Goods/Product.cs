using UnityEngine;

[CreateAssetMenu(fileName = "DefaultProduct", menuName = "Goods/Product")]
public class Product : ScriptableObject
{
    public string Name = "Пусто";
    public ProductType Type = ProductType.Burger;
    public int Cost;

    public bool IsContainer;
    //public int ContainedCount;
    public ProductType ContainedType = ProductType.Burger;

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
