using UnityEngine;

[RequireComponent(typeof(Courier))]
public class CourierUpgradeQueue : MonoBehaviour
{
    [SerializeField] Sprite[] uiIcons;
    [SerializeField] Sprite[] worldIcons;
    [SerializeField] private CourierUpgrade[] upgrades;
    private UpgradeQueue _upgradeQueue;

    public UpgradeQueue UpgradeQueue
    {
        get
        {
            if (_upgradeQueue == null)
                _upgradeQueue = new UpgradeQueue(upgrades, transform, uiIcons, worldIcons);

            return _upgradeQueue;
        }
    }
    public BaseUpgrade CurrentUpgrade => UpgradeQueue.CurrentUpgrade;

    private void Awake()
    {
        if (_upgradeQueue == null)
            _upgradeQueue = new UpgradeQueue(upgrades, transform, uiIcons, worldIcons);
    }
}
