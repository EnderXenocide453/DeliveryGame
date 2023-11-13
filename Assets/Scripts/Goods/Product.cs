[System.Serializable]
public class Product
{
    public ProductType Type;
    public string Name;
    public int Count;
    public float Cost;

    public Product Clone { get => new Product(Type, Name, Count, Cost); }

    public Product(ProductType type, string name, int count, float cost)
    {
        Type = type;
        Name = name;
        Count = count;
        Cost = cost;
    }
}

public enum ProductType
{
    Burger,
    Pizza,
    Sushi,
    Drink,
    Flower
}
