using UnityEngine;

[RequireComponent(typeof(Courier))]
public class CourierUpgradeQueue : MonoBehaviour
{
    [SerializeField] private CourierUpgrade[] upgrades;
    private UpgradeQueue _upgradeQueue;

    public BaseUpgrade CurrentUpgrade { get => _upgradeQueue.CurrentUpgrade; }

    private void Awake()
    {
        _upgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}
