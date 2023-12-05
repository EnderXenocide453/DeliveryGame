using UnityEngine;

public class AddMoney : MonoBehaviour
{
    [SerializeField] private GameObject addMoneyMenu;
    [SerializeField] private PlayerMovement movement;
    private void Start() => addMoneyMenu.SetActive(false);
    public void OnClick()
    {
        addMoneyMenu.SetActive(true);
        movement.ChangeMove(false);
    }

    public void ExitClick()
    {
        addMoneyMenu.SetActive(false);
        movement.ChangeMove(true);
    }

    public void OneHundredBucksClick()
    {
        GlobalValueHandler.Cash += 100;
        Debug.Log("Added 100");
    }
    public void OneThousandBucksClick()
    {
        GlobalValueHandler.Cash += 1000;
        Debug.Log("Added 1000");
    }
    public void TwoThousandBucksClick()
    {
        GlobalValueHandler.Cash += 2000;
        Debug.Log("Added 2000");
    }
    public void FiveThousandBucksClick()
    {
        GlobalValueHandler.Cash += 5000;
        Debug.Log("Added 5000");
    }
}
