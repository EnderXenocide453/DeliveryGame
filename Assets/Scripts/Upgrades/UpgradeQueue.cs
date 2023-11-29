using System.Collections.Generic;
using UnityEngine;

public class UpgradeQueue
{
    public bool isLocked { get; private set; }
    public int currentID { get; private set; }

    private BaseUpgrade[] _upgrades;
    private Sprite[] _uiIcons;
    private Sprite[] _worldIcons;

    public BaseUpgrade CurrentUpgrade { get => currentID >= _upgrades.Length ? _upgrades[_upgrades.Length - 1] : _upgrades[currentID]; }
    public Sprite CurrentUIIcon { get => currentID >= _uiIcons.Length ? _uiIcons[_uiIcons.Length - 1] : _uiIcons[currentID]; }
    public Sprite CurrentWorldIcon { get => currentID >= _worldIcons.Length ? _worldIcons[_worldIcons.Length - 1] : _worldIcons[currentID]; }

    public event BaseUpgrade.UpgradeEventHandler onUpgraded;
    public event BaseUpgrade.UpgradeEventHandler onLocked;
    public event BaseUpgrade.UpgradeEventHandler onUnlocked;
    public event BaseUpgrade.UpgradeEventHandler onMaxLevelReached;

    public UpgradeQueue(BaseUpgrade[] upgrades, Transform parent, Sprite[] uiIcons, Sprite[] worldIcons = null)
    {
        _upgrades = new BaseUpgrade[upgrades.Length];
        _uiIcons = new Sprite[upgrades.Length + 1];
        _worldIcons = new Sprite[upgrades.Length + 1];

        upgrades.CopyTo(_upgrades, 0);

        for (int i = 1; i < _upgrades.Length; i++) {
            _upgrades[i - 1].nextUpgrade = _upgrades[i];
            _upgrades[i].onUpgraded += OnUpgraded;

            _upgrades[i].SetTarget(parent);
        }

        _upgrades[0].onUpgraded += OnUpgraded;
        _upgrades[0].SetTarget(parent);

        for (int i = 0; i < _uiIcons.Length; i++) {
            _uiIcons[i] = i >= uiIcons.Length ? uiIcons[uiIcons.Length - 1] : uiIcons[i];

            if (worldIcons == null)
                _worldIcons[i] = _uiIcons[i];
            else
                _worldIcons[i] = i >= worldIcons.Length ? worldIcons[worldIcons.Length - 1] : worldIcons[i];
        }
    }

    public UpgradeQueue(BaseUpgrade[] upgrades, Transform parent, Sprite uiIcon, Sprite worldIcon = null)
    {
        _upgrades = new BaseUpgrade[upgrades.Length];
        _uiIcons = new Sprite[upgrades.Length + 1];
        _worldIcons = new Sprite[upgrades.Length + 1];

        upgrades.CopyTo(_upgrades, 0);

        for (int i = 1; i < _upgrades.Length; i++) {
            _upgrades[i - 1].nextUpgrade = _upgrades[i];
            _upgrades[i].onUpgraded += OnUpgraded;

            _upgrades[i].SetTarget(parent);
        }

        _upgrades[0].onUpgraded += OnUpgraded;
        _upgrades[0].SetTarget(parent);

        for (int i = 0; i < _uiIcons.Length; i++) {
            _uiIcons[i] = uiIcon;
            _worldIcons[i] = worldIcon ? worldIcon : uiIcon;
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
