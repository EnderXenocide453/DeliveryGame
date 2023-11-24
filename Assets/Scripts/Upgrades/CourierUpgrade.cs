using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CourierUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;
    [SerializeField] float speedModifier;

    //Доступные виды товара
    //Новая иконка и изменения скина

    private Courier _target;

    public override string Name => name;

    public override string Description => $"Скорость: {speedModifier}\nВместимость: {storageCapacity}";

    public override void SetTarget(Transform target)
    {
        _target = target.GetComponent<Courier>();
    }

    protected override void PostUpgrade()
    {
        _target.mapSpeedModifier = speedModifier;
        _target.CourierStorage.MaxCount = storageCapacity;
    }

    protected override void PreUpgrade() { }
}
