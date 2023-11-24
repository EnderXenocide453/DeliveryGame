using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUpgradeQueue : MonoBehaviour
{
    [SerializeField] private StorageUpgrade[] upgrades;
    public UpgradeQueue UpgradeQueue { get; private set; }

    public BaseUpgrade CurrentUpgrade 
    {
        get
        {
            if (UpgradeQueue == null)
                UpgradeQueue = new UpgradeQueue(upgrades, transform);

            return UpgradeQueue.CurrentUpgrade;
        }
    }

    private void Awake()
    {
        if (UpgradeQueue == null)
            UpgradeQueue = new UpgradeQueue(upgrades, transform);

        Debug.Log("storage");
    }

    public void LockUpgrades()
    {
        UpgradeQueue.SetLock(true);
    }

    public void UnlockUpgrades()
    {
        UpgradeQueue.SetLock(false);
    }
}

