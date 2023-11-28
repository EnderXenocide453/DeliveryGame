using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Storage))]
public class PlayerUpgradeQueue : MonoBehaviour
{
    [SerializeField] private Sprite icon;
    [SerializeField] private PlayerUpgrade[] upgrades;
    private UpgradeQueue _upgradeQueue;

    public UpgradeQueue UpgradeQueue
    {
        get
        {
            if (_upgradeQueue == null)
                _upgradeQueue = new UpgradeQueue(upgrades, transform, icon);

            return _upgradeQueue;
        }
    }
    public BaseUpgrade CurrentUpgrade => UpgradeQueue.CurrentUpgrade;

    private void Awake()
    {
        if (_upgradeQueue == null)
            _upgradeQueue = new UpgradeQueue(upgrades, transform, icon);
    }
}