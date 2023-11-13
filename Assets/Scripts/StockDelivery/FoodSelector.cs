using UnityEngine;
using UnityEngine.UI;

public class FoodSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] foodList;
    [SerializeField] private Button takePositionButton;
    [SerializeField] private CharacterController characterController;
    private void Start()
    {
        takePositionButton.onClick.AddListener(OnTakePositionButtonClick);
    }

    private void OnTakePositionButtonClick()
    {
        GameObject selectedFood = GetRandomFood();

        if (characterController != null && selectedFood != null)
        {
            characterController.TakeFood(selectedFood);
        }
    }
    private GameObject GetRandomFood()
    {
        if (foodList.Length == 0)
        {
            Debug.LogWarning("Food array is empty!");
            return null;
        }

        int randomIndex = Random.Range(0, foodList.Length);
        return foodList[randomIndex];
    }
}
