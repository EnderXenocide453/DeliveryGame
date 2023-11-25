using System.Collections.Generic;
using UnityEngine;

public class UpgradeQueue
{
    public bool isLocked { get; private set; }
    public int currentID { get; private set; }

    private BaseUpgrade[] _upgrades;

    public BaseUpgrade CurrentUpgrade { get => currentID >= _upgrades.Length ? _upgrades[_upgrades.Length - 1] : _upgrades[currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;
    public event BaseUpgrade.UpgradeEventHandler onLocked;
    public event BaseUpgrade.UpgradeEventHandler onUnlocked;
    public event BaseUpgrade.UpgradeEventHandler onMaxLevelReached;

    public UpgradeQueue(BaseUpgrade[] upgrades, Transform parent)
    {
        _upgrades = new BaseUpgrade[upgrades.Length];
        upgrades.CopyTo(_upgrades, 0);

        for (int i = 1; i < _upgrades.Length; i++) {
            _upgrades[i - 1].nextUpgrade = _upgrades[i];
            _upgrades[i].onUpgraded += OnUpgraded;

            _upgrades[i].SetTarget(parent);
        }

        _upgrades[0].onUpgraded += OnUpgraded;
        _upgrades[0].SetTarget(parent);
    }

    public void SetLock(bool locked)
    {
        (isLocked, locked) = (locked, isLocked);

        if (isLocked != locked) {
            if (isLocked)
                onLocked?.Invoke();
            else
                onUnlocked?.Invoke();
        }
    }

    public void UpgradeTo(int level)
    {
        if (level < 0) {
            SetLock(true);
            return;
        }

        if (level == 0)
            return;

        level = Mathf.Clamp(level, 0, _upgrades.Length - 1);
        currentID = 0;

        for (int i = 0; i < level; i++)
            CurrentUpgrade.DoUpgrade();

        onUpgraded?.Invoke();
    }

    private void OnUpgraded()
    {
        currentID++;

        if (currentID >= _upgrades.Length)
            onMaxLevelReached?.Invoke();

        onUpgraded?.Invoke();
    }
}
