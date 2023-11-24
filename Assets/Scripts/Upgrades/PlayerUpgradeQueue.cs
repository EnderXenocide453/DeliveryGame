using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(Storage))]
public class PlayerUpgradeQueue : MonoBehaviour
{
    [SerializeField] private PlayerUpgrade[] upgrades;
    public UpgradeQueue UpgradeQueue { get; private set; }

    public BaseUpgrade CurrentUpgrade { get => UpgradeQueue.CurrentUpgrade; }

    private void Awake()
    {
        UpgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}