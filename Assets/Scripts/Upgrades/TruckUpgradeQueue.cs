using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckUpgradeQueue : MonoBehaviour
{
    [SerializeField] private TruckUpgrade[] upgrades;
    private UpgradeQueue _upgradeQueue;

    public UpgradeQueue UpgradeQueue
    {
        get
        {
            if (_upgradeQueue == null)
                _upgradeQueue = new UpgradeQueue(upgrades, transform);

            return _upgradeQueue;
        }
    }
    public BaseUpgrade CurrentUpgrade => UpgradeQueue.CurrentUpgrade;

    private void Awake()
    {
        if (_upgradeQueue == null)
            _upgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}