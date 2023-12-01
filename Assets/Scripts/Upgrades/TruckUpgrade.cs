using UnityEngine;

[System.Serializable]
public class TruckUpgrade : BaseUpgrade
{
    [SerializeField] int inBoxCount;

    public override string Name => "Грузовик";

    public override string Description => $"Вместимость коробки: {GoodsManager.BoxCount}";

    public override void SetTarget(Transform parent) { }

    protected override void PostUpgrade()
    {
        
    }

    protected override void PreUpgrade()
    {
        GoodsManager.BoxCount = inBoxCount;
    }
}
