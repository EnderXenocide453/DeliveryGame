using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CourierUpgrade : BaseUpgrade
{
    [SerializeField] int storageCapacity;
    [SerializeField] float speedModifier;

    //��������� ���� ������
    //����� ������ � ��������� �����

    private Courier _target;

    public override string Name => $"������ {_target.ID + 1}";

    public override string Description => $"��������: {_target.mapSpeedModifier}\n�����������: {_target.CourierStorage.MaxCount}";

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
        _target.CourierStorage.MaxCount = storageCapacity;
    }
}
