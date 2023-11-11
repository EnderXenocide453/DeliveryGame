using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private GameObject collectFoodButton;
    [SerializeField] private Transform collectionZone;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CollectFood"))
        {
            collectFoodButton.SetActive(true);
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
        GameObject foodType = Instantiate(foodTypePrefab, collectionZone.position, Quaternion.identity);
        foodType.transform.SetParent(transform, true);
    }
}
