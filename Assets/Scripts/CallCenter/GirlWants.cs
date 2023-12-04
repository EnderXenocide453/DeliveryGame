using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GirlWants : MonoBehaviour
{
    [SerializeField] private float displayInterval = 120;
    [SerializeField] private EmojiCloud cloud;

    [SerializeField] private LayerMask playerMask;

    [SerializeField] private InAreaRandom bonusAreas;
    [SerializeField] private GameObject bonusObject;
    [SerializeField] private float bonusSpeed = 0.5f;
    [SerializeField] private float bonusArkHeight = 0.5f;

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

            int randomTypeOfFood = UnityEngine.Random.Range(0, OrdersManager.goodsTypes.Count);
            _wantedType = OrdersManager.goodsTypes[randomTypeOfFood];

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
            Apply();
        }
    }

    [ContextMenu("Create bonus")]
    private void Apply()
    {
        _wannaSomething = false;

        Instantiate(bonusObject, transform.position, Quaternion.identity).GetComponent<Bonus>().FlyTo(bonusAreas.GetRandomPoint(), bonusSpeed, bonusArkHeight);
        cloud.Clear();
    }

    private void Deny()
    {

    }
}