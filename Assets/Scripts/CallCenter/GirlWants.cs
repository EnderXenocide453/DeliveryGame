using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GirlWants : MonoBehaviour
{
    [SerializeField] private float displayInterval = 10f;
    [Space]
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private Storage characterInventory;

    [SerializeField] private List<ProductType> wantedTypes;

    private ProductType _wantedType;
    private void Start()
    {
        StartCoroutine(DisplayDesiredItem());
    }
    private IEnumerator DisplayDesiredItem()
    {
        while (true)
        {
            int randomTypeOfFood = UnityEngine.Random.Range(0, wantedTypes.Count);

            image.overrideSprite = GoodsManager.GetProductInfo(wantedTypes[randomTypeOfFood]).Icon;
            _wantedType = wantedTypes[randomTypeOfFood];
            yield return new WaitForSeconds(displayInterval);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vibration.SingleVibration();
            if (characterInventory.GetProductCount(_wantedType) >= 1)
            {
                characterInventory.RemoveProduct(_wantedType, 1);
            }
        }
    }
}