using UnityEngine;

public class StoreUpgradeUI : BaseUpgradeUI
{
    [Space]
    [SerializeField] private TruckUpgradeQueue truckUpgrade;
    [SerializeField] StorageUpgradeQueue[] storageUpgrades;

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
        //var upgrades = FindObjectsOfType<StorageUpgradeQueue>(true);

        for (int i = 0; i < storageUpgrades.Length; i++)
            AddStorageUpgrade(storageUpgrades[i]);
    }

    private void AddStorageUpgrade(StorageUpgradeQueue queue)
    {
        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(queue.UpgradeQueue);
    }
}
