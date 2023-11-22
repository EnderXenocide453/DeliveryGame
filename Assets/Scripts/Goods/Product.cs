using UnityEngine;

[System.Serializable]
public class Product
{
    public string Name;
    public ProductType Type;
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
