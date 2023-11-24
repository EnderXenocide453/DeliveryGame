using UnityEngine;

public class CourierUpgradeUI : BaseUpgradeUI
{
    [Space]
    [SerializeField] private int startCost;
    [SerializeField] private int costIncrement = 100;
    [SerializeField] private TMPro.TMP_Text counter;
    [SerializeField] private UnityEngine.UI.Button hireButton;

    private int _currentCost;

    private void Awake()
    {
        _currentCost = startCost;
        GlobalValueHandler.onCashChanged += UpdateBtn;
    }
    
    public void OnHireButtonClick()
    {
        GlobalValueHandler.Cash -= _currentCost;
        var queue = CourierManager.instance.AddNewCourier().GetComponent<CourierUpgradeQueue>();
        AddUpgradePanel(queue.UpgradeQueue);

        if (CourierManager.isMaxCouriers) {
            GlobalValueHandler.onCashChanged -= UpdateBtn;
            hireButton.interactable = false;
            counter.text = "Max";

            return;
        }

        _currentCost += costIncrement;
        counter.text = $"Нанять: {_currentCost}";
        UpdateBtn();
    }

    private void UpdateBtn()
    {
        hireButton.interactable = GlobalValueHandler.Cash >= _currentCost;
    }
}
