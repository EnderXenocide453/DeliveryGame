using UnityEngine;

[System.Serializable]
public class Product
{
    public ProductType Type;
    public string Name;
    public int Count;
    public int Cost;

    public GameObject Prefab;
    public Sprite Icon;
}

public enum ProductType
{
    Burger,
    Pizza,
    Sushi,
    Drink,
    Flower
}
