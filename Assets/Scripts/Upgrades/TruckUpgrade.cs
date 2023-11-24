using UnityEngine;

[System.Serializable]
public class TruckUpgrade : BaseUpgrade
{
    [SerializeField] int inBoxCount;

    public override string Name => "�������� ��������";

    public override string Description => $"����������� ��������������� ������� �� {inBoxCount}";

    public override void SetTarget(Transform parent) { }

    protected override void PostUpgrade()
    {
        GoodsManager.BoxCount = inBoxCount;
    }

    protected override void PreUpgrade() { }
}
