using UnityEngine;

[System.Serializable]
public class StorageUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;

    private Storage _target;

    public override string Name => name;

    public override string Description => $"Вместимость: {storageCapacity}";

    public override void SetTarget(Transform target)
    {
        _target = target.GetComponent<Storage>();
    }

    protected override void PostUpgrade()
    {
        _target.MaxCount = storageCapacity;

        OrdersManager.ChangeMaxCount(storageCapacity);
    }

    protected override void PreUpgrade() { }
}
