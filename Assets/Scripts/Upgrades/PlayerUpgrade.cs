using UnityEngine;

[System.Serializable]
public class PlayerUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;
    [SerializeField] float speedModifier;

    private PlayerMovement _target;
    private Storage _targetStorage;

    public override string Name => "Кладовщик";

    public override string Description => $"Скорость: {_target.speedModifier}\nВместимость: {_targetStorage.MaxCount}";

    public override void SetTarget(Transform target)
    {
        _target = target.GetComponent<PlayerMovement>();
        _targetStorage = target.GetComponent<Storage>();
    }

    protected override void PostUpgrade()
    {
        
    }

    protected override void PreUpgrade()
    {
        _target.speedModifier = speedModifier;
        _targetStorage.MaxCount = storageCapacity;

        OrdersManagement.OrdersManager.ChangeMaxCount(storageCapacity);
    }
}
