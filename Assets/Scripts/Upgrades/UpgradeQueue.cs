using System.Collections.Generic;
using UnityEngine;

public class UpgradeQueue
{
    public bool isLocked { get; private set; }
    public int currentID { get; private set; }

    private BaseUpgrade[] _upgrades;
    private Sprite[] _icons;

    public BaseUpgrade CurrentUpgrade { get => currentID >= _upgrades.Length ? _upgrades[_upgrades.Length - 1] : _upgrades[currentID]; }
    public Sprite CurrentIcon { get => currentID >= _icons.Length ? _icons[_icons.Length - 1] : _icons[currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;
    public event BaseUpgrade.UpgradeEventHandler onLocked;
    public event BaseUpgrade.UpgradeEventHandler onUnlocked;
    public event BaseUpgrade.UpgradeEventHandler onMaxLevelReached;

    public UpgradeQueue(BaseUpgrade[] upgrades, Transform parent, Sprite[] icons)
    {
        _upgrades = new BaseUpgrade[upgrades.Length];
        _icons = new Sprite[upgrades.Length + 1];
        upgrades.CopyTo(_upgrades, 0);

        for (int i = 1; i < _upgrades.Length; i++) {
            _upgrades[i - 1].nextUpgrade = _upgrades[i];
            _upgrades[i].onUpgraded += OnUpgraded;

            _upgrades[i].SetTarget(parent);
        }

        _upgrades[0].onUpgraded += OnUpgraded;
        _upgrades[0].SetTarget(parent);

        for (int i = 0; i < _icons.Length; i++) {
            _icons[i] = i >= icons.Length ? icons[icons.Length - 1] : icons[i];
        }
    }

    public UpgradeQueue(BaseUpgrade[] upgrades, Transform parent, Sprite icon)
    {
        _upgrades = new BaseUpgrade[upgrades.Length];
        _icons = new Sprite[upgrades.Length + 1];
        upgrades.CopyTo(_upgrades, 0);

        for (int i = 1; i < _upgrades.Length; i++) {
            _upgrades[i - 1].nextUpgrade = _upgrades[i];
            _upgrades[i].onUpgraded += OnUpgraded;

            _upgrades[i].SetTarget(parent);
        }

        _upgrades[0].onUpgraded += OnUpgraded;
        _upgrades[0].SetTarget(parent);

        for (int i = 0; i < _icons.Length; i++) {
            _icons[i] = icon;
        }
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

        level = Mathf.Clamp(level, 0, _upgrades.Length);
        currentID = 0;

        while (currentID < level) {
            CurrentUpgrade.DoUpgrade();
        }

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
