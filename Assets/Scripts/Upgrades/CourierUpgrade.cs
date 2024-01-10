using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CourierUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;
    [SerializeField] float speedModifier;

    [SerializeField] Transform[] activeParts;
    [SerializeField] Transform[] inactiveParts;

    private Courier _target;

    public override string Name => $"Курьер {_target.ID + 1}";

    public override string Description => $"Скорость: {_target.mapSpeedModifier}\nВместимость: {_target.maxGoodsCount}";

    public override void SetTarget(Transform target)
    {
        _target = target.GetComponent<Courier>();
    }

    protected override void PostUpgrade()
    {
    }

    protected override void PreUpgrade()
    {
        _target.mapSpeedModifier = speedModifier;
        _target.maxGoodsCount = storageCapacity;

        foreach (var part in activeParts) {
            part.gameObject.SetActive(true);
        }
        foreach (var part in inactiveParts) {
            part.gameObject.SetActive(false);
        }
    }
}
