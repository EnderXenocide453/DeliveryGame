using System.Collections.Generic;
using UnityEngine;

public class UpgradeQueue
{
    private bool _isLocked;

    private BaseUpgrade[] _upgrades;
    private int _currentID;

    public BaseUpgrade CurrentUpgrade { get => _currentID >= _upgrades.Length ? null : _upgrades[_currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;
    public event BaseUpgrade.UpgradeEventHandler onLocked;
    public event BaseUpgrade.UpgradeEventHandler onUnlocked;

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
        if (_isLocked != locked) {
            if (_isLocked)
                onLocked?.Invoke();
            else
                onUnlocked?.Invoke();
        }

        _isLocked = locked;
    }

    private void OnUpgraded()
    {
        _currentID++;

        onUpgraded?.Invoke();
    }
}
