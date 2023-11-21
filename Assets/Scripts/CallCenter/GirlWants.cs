using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GirlWants : MonoBehaviour
{
    [SerializeField] private float displayInterval = 10f;
    [Space]
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private List<Sprite> newSprite;
    [Space]
    [SerializeField] private Storage characterInventory;

    private ProductType _wantedType;
    private void Start()
    {
        StartCoroutine(DisplayDesiredItem());
    }
    private IEnumerator DisplayDesiredItem()
    {
        while (true)
        {
            int randomTypeOfFood = UnityEngine.Random.Range(0, 5);

            image.gameObject.SetActive(true);
            image.overrideSprite = newSprite[randomTypeOfFood];
            _wantedType = (ProductType)randomTypeOfFood;
            yield return new WaitForSeconds(displayInterval);
            image.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var productType in characterInventory.StoredProducts)
            {
                if (_wantedType == productType.Key && productType.Value >= 1)
                {
                    characterInventory.gameObject.GetComponentInChildren<GoodsStackItem>().gameObject.SetActive(false);
                }
            }

        }
    }
}