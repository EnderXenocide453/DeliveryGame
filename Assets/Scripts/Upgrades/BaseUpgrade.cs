using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class BaseUpgrade
{
    public string name;
    [TextArea] public string description;
    public int cost;

    public BaseUpgrade nextUpgrade;

    protected int currentLevel;

    public delegate void UpgradeEventHandler();
    public event UpgradeEventHandler onUpgraded;

    public void DoUpgrade()
    {
        PreUpgrade();
        onUpgraded?.Invoke();
        onUpgraded = null;
        PostUpgrade();
    }

    protected abstract void PreUpgrade();
    protected abstract void PostUpgrade();
}
