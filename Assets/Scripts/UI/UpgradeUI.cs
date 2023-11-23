using System.Collections.Generic;
using UnityEngine;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject contentField;
    [SerializeField] private GameObject upgradeRowPrefab;
    [Space]
    [SerializeField] private int startCost;
    [SerializeField] private int costIncrement = 100;
    [SerializeField] private TMPro.TMP_Text counter;
    [SerializeField] private UnityEngine.UI.Button hireButton;

    private int _currentCost;

    private void Awake()
    {
        AddPlayerUpgradePanel();

        _currentCost = startCost;
        GlobalValueHandler.onCashChanged += UpdateBtn;
    }

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
    public void OnHireButtonClick()
    {
        AddCourierUpgradePanel();
    }

    private void AddPlayerUpgradePanel()
    {
        UpgradePanel playerPanel = Addpanel();
        playerPanel.AttachUpgrade(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUpgradeQueue>().CurrentUpgrade);
    }

    private void AddCourierUpgradePanel()
    {
        GlobalValueHandler.Cash -= _currentCost;

        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(CourierManager.instance.AddNewCourier().GetComponent<CourierUpgradeQueue>().CurrentUpgrade);

        if (CourierManager.isMaxCouriers) {
            GlobalValueHandler.onCashChanged -= UpdateBtn;
            hireButton.interactable = false;
            counter.text = "Max";
            
            return;
        }

        _currentCost += costIncrement;
        counter.text = $"������: {_currentCost}";
        UpdateBtn();
    }

    private UpgradePanel Addpanel() => Instantiate(upgradeRowPrefab, contentField.transform).GetComponent<UpgradePanel>();

    private void UpdateBtn()
    {
        hireButton.interactable = GlobalValueHandler.Cash >= _currentCost;
    }
}
