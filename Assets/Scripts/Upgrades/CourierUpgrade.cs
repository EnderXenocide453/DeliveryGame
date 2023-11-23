using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, RequireComponent(typeof(Courier))]
public class CourierUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;
    [SerializeField] float speedModifier;

    //Доступные виды товара
    //Новая иконка и изменения скина

    private Courier _target;

    public void SetTarget(Courier target)
    {
        _target = target;
    }

    protected override void PostUpgrade()
    {
        _target.speedModifier = speedModifier;
        _target.CourierStorage.MaxCount = storageCapacity;
    }

    protected override void PreUpgrade() { }
}
