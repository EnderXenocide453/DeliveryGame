using UnityEngine;

[RequireComponent(typeof(Courier))]
public class CourierUpgradeQueue : MonoBehaviour
{
    [SerializeField] private CourierUpgrade[] upgrades;
    private int _currentID;
    private Courier _target;

    public BaseUpgrade CurrentUpgrade { get => _currentID >= upgrades.Length ? null : upgrades[_currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (upgrades.Length == 0)
            return;

        for (int i = 1; i < upgrades.Length; i++) {
            upgrades[i - 1].nextUpgrade = upgrades[i];
        }

        upgrades[0].onUpgraded += OnUpgraded;

        _target = GetComponent<Courier>();
    }

    private void OnUpgraded()
    {
        var upgrade = (CourierUpgrade)CurrentUpgrade;
        upgrade.SetTarget(_target);
        _currentID++;

        onUpgraded?.Invoke();
    }
}
