using UnityEngine;

[RequireComponent(typeof(Courier))]
public class CourierUpgradeQueue : MonoBehaviour
{
    [SerializeField] Sprite[] icons;
    [SerializeField] private CourierUpgrade[] upgrades;
    private UpgradeQueue _upgradeQueue;

    public UpgradeQueue UpgradeQueue
    {
        get
        {
            if (_upgradeQueue == null)
                _upgradeQueue = new UpgradeQueue(upgrades, transform, icons);

            return _upgradeQueue;
        }
    }
    public BaseUpgrade CurrentUpgrade => UpgradeQueue.CurrentUpgrade;

    private void Awake()
    {
        if (_upgradeQueue == null)
            _upgradeQueue = new UpgradeQueue(upgrades, transform, icons);
    }
}
