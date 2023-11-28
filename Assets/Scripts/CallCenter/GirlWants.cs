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

    private ProductType _wantedType;

    private void Start()
    {
        StartCoroutine(DisplayDesiredItem());
    }

    private IEnumerator DisplayDesiredItem()
    {
        while (true)
        {
            int randomTypeOfFood = UnityEngine.Random.Range(0, OrdersManager.goodsTypes.Count);
            _wantedType = OrdersManager.goodsTypes[randomTypeOfFood];

            cloud.DrawImage(GoodsManager.GetProductInfo(_wantedType).Icon, displayInterval);
            yield return new WaitForSeconds(displayInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<Storage>(out var storage))
        {
            if (storage.CurrentCount > 1 || storage.GetProductCount(_wantedType) != 1) {
                Deny();
                return;
            }

            storage.RemoveProduct(_wantedType, 1);
            Apply();
        }
    }

    private void Apply()
    {
        Instantiate(bonusObject, bonusAreas.GetRandomPoint(), Quaternion.identity);
        cloud.Clear();
    }

    private void Deny()
    {

    }
}