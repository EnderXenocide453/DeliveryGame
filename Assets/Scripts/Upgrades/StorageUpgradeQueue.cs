using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageUpgradeQueue : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private StorageUpgrade[] upgrades;
    [SerializeField] private bool lockAtStart;

    public int ID;

    private UpgradeQueue _upgradeQueue;

    public UpgradeQueue UpgradeQueue 
    { 
        get
        {
            if (_upgradeQueue == null)
                InitQueue();

            return _upgradeQueue;
        } 
    }
    public BaseUpgrade CurrentUpgrade => UpgradeQueue.CurrentUpgrade;

    private void OnEnable()
    {
        UpgradeQueue.SetLock(false);
    }

    public void LockUpgrades()
    {
        UpgradeQueue.SetLock(true);
    }

    public void UnlockUpgrades()
    {
        UpgradeQueue.SetLock(false);
    }

    private void InitQueue()
    {
        _upgradeQueue = new UpgradeQueue(upgrades, transform, icon);

        _upgradeQueue.SetLock(lockAtStart);
    }
}

