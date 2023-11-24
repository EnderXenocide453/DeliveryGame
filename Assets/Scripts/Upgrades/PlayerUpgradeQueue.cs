using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Storage))]
public class PlayerUpgradeQueue : MonoBehaviour
{
    [SerializeField] private PlayerUpgrade[] upgrades;

    private UpgradeQueue _upgradeQueue;

    public BaseUpgrade CurrentUpgrade { get => _upgradeQueue.CurrentUpgrade; }

    private void Awake()
    {
        _upgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}