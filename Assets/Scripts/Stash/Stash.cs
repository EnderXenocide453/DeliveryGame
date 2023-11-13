using UnityEngine;
using UnityEngine.UI;

public class Stash : MonoBehaviour
{
    [SerializeField] private Product getProductType;

    //[HideInInspector]
    public StashType stashType;

    //[HideInInspector]
    public int productCount = 0;

    //[HideInInspector]
    public bool match;

    public void OnPutProductButtonClick()
    {
        if (stashType.ToString().ToLower() == getProductType.productType.ToLower())
        {
            match = true;
            productCount++;
            Debug.Log("is available");
        }
        else
        {
            Debug.Log("is not available");
        }
    }
}

public enum StashType
{
    pizza,
    sushi,
    burger,
    drink,
    flower
}