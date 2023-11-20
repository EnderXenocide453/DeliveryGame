using System.Collections.Generic;
using UnityEngine;

public class CouriersUI : MonoBehaviour
{
    [SerializeField] private GameObject contentField;
    [SerializeField] private GameObject upgradeRowPrefab;
    [Space]
    [SerializeField] private float startCost;
    [SerializeField] private float costIncrement = 100;
    [SerializeField] private TMPro.TMP_Text counter;
    [SerializeField] private UnityEngine.UI.Button hireButton;

    private float _currentCost;

    private void Awake()
    {
        AddPlayerUpgradePanel();

        _currentCost = startCost;
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
        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(CourierManager.instance.AddNewCourier().GetComponent<CourierUpgradeQueue>().CurrentUpgrade);

        _currentCost += costIncrement;
        counter.text = $"Нанять: {_currentCost}";
        UpdateBtn();
    }

    private UpgradePanel Addpanel() => Instantiate(upgradeRowPrefab, contentField.transform).GetComponent<UpgradePanel>();

    private void UpdateBtn()
    {
        hireButton.interactable = GlobalValueHandler.Cash >= _currentCost;
    }
}
