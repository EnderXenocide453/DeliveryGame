using UnityEngine;

[System.Serializable]
public class TruckUpgrade : BaseUpgrade
{
    [SerializeField] int inBoxCount;

    public override string Name => "Улучшить грузовик";

    public override string Description => $"Увеличивает вместительность коробки до {inBoxCount}";

    public override void SetTarget(Transform parent) { }

    protected override void PostUpgrade()
    {
        GoodsManager.BoxCount = inBoxCount;
    }

    protected override void PreUpgrade() { }
}
