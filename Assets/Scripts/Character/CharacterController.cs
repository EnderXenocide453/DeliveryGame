using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Collect food")]
    [SerializeField] private GameObject collectFoodButton;
    [SerializeField] private Transform collectionZone;
    [Header("Put to the fridge")]
     public Stash stash;
    [SerializeField] private Product product;
    [Header("Give an order")]
    [SerializeField] private Stall orderRequet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectFood"))
        {
            collectFoodButton.SetActive(true);
        }
        if (other.CompareTag("StashSet"))
        {
            Debug.Log("Stash set");
            stash.OnPutProductButtonClick();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CollectFood"))
        {
            collectFoodButton.SetActive(false);
        }
    }

    public void TakeFood(GameObject foodTypePrefab)
    {
        product.productType = foodTypePrefab.name;
        GameObject foodType = Instantiate(foodTypePrefab, collectionZone.position, Quaternion.identity);
        foodType.transform.SetParent(transform, true);
    }
}
