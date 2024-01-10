using System.Collections;
using UnityEngine;

public class GirlWants : MonoBehaviour
{
    [SerializeField] private float displayInterval = 120;
    [SerializeField] private EmojiCloud cloud;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private GameObject bonusObject;
    [SerializeField] private float bonusSpeed = 0.5f;

    private ProductType _wantedType;
    private bool _wannaSomething;

    private void Start()
    {
        StartCoroutine(DisplayDesiredItem());
    }

    private IEnumerator DisplayDesiredItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(displayInterval);

            int randomTypeOfFood = UnityEngine.Random.Range(0, OrdersManagement.OrdersManager.goodsTypes.Count);
            _wantedType = OrdersManagement.OrdersManager.goodsTypes[randomTypeOfFood];

            cloud.DrawImage(GoodsManager.GetProductInfo(_wantedType).Icon, displayInterval);
            _wannaSomething = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_wannaSomething && ((1 << other.gameObject.layer) & playerMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<Storage>(out var storage))
        {
            if (storage.CurrentCount > 1 || storage.GetProductCount(_wantedType) != 1) {
                Deny();
                return;
            }

            storage.RemoveProduct(_wantedType, 1);
            Apply(other.attachedRigidbody.transform);
        }
    }

    [ContextMenu("Create bonus")]
    private void Apply(Transform target)
    {
        _wannaSomething = false;

        Instantiate(bonusObject, transform.position, Quaternion.identity).GetComponent<Bonus>().FlyTo(target, bonusSpeed);
        cloud.Clear();

        SoundsManager.PlaySound(SoundsManager.instance.kissSound);
    }

    private void Deny()
    {

    }
}