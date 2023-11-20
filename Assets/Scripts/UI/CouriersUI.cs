using System.Collections.Generic;
using UnityEngine;

public class CouriersUI : MonoBehaviour
{
    [SerializeField] private GameObject contentField;
    [SerializeField] private GameObject upgradeRowPrefab;

    private void Awake()
    {
        AddPlayerUpgradePanel();
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
    }

    private UpgradePanel Addpanel() => Instantiate(upgradeRowPrefab, contentField.transform).GetComponent<UpgradePanel>();
}
