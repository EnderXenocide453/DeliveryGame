using UnityEngine;

[RequireComponent(typeof(Courier))]
public class CourierUpgradeQueue : MonoBehaviour
{
    [SerializeField] private CourierUpgrade[] upgrades;
    public UpgradeQueue UpgradeQueue { get; private set; }

    public BaseUpgrade CurrentUpgrade { get => UpgradeQueue.CurrentUpgrade; }

    private void Awake()
    {
        UpgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}
