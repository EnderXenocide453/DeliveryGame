using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckUpgradeQueue : MonoBehaviour
{
    [SerializeField] private TruckUpgrade[] upgrades;
    public UpgradeQueue UpgradeQueue { get; private set; }

    public BaseUpgrade CurrentUpgrade { get => UpgradeQueue.CurrentUpgrade; }

    private void Awake()
    {
        UpgradeQueue = new UpgradeQueue(upgrades, transform);
    }
}
