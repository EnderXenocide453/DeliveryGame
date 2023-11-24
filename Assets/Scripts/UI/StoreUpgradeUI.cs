using UnityEngine;

public class StoreUpgradeUI : BaseUpgradeUI
{
    [Space]
    [SerializeField] private TruckUpgradeQueue truckUpgrade;

    private void Awake()
    {
        AddPlayerUpgrade();
        AddTruckUpgrade();
        AddAllStoragesUpgrades();
    }

    private void AddPlayerUpgrade()
    {
        UpgradePanel playerPanel = Addpanel();
        playerPanel.AttachUpgrade(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUpgradeQueue>().UpgradeQueue);
    }

    private void AddTruckUpgrade()
    {
        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(truckUpgrade.UpgradeQueue);
    }

    private void AddAllStoragesUpgrades()
    {
        var upgrades = FindObjectsOfType<StorageUpgradeQueue>(true);

        for (int i = upgrades.Length - 1; i >= 0; i--)
            AddStorageUpgrade(upgrades[i]);
    }

    private void AddStorageUpgrade(StorageUpgradeQueue queue)
    {
        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(queue.UpgradeQueue);
    }
}
