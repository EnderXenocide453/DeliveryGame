using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseUpgrade
{
    public int cost;
    public BaseUpgrade nextUpgrade;

    protected int currentLevel;

    public abstract string Name { get; }
    public abstract string Description { get; }

    public delegate void UpgradeEventHandler();
    public event UpgradeEventHandler onUpgraded;

    public void DoUpgrade()
    {
        PreUpgrade();
        onUpgraded?.Invoke();
        onUpgraded = null;
        PostUpgrade();
    }

    public abstract void SetTarget(Transform parent);
    protected abstract void PreUpgrade();
    protected abstract void PostUpgrade();
}
