using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildArea : InteractableArea
{
    [SerializeField] int cost;
    [SerializeField] Transform[] buildParts;
    [SerializeField] float cashSpendDelay = 0.05f;
    [SerializeField] int cashSpendCount = 1;
    [SerializeField] TMPro.TMP_Text cashCounter;
    [Space]
    [SerializeField] ProductType allowedType;
    [SerializeField] bool buildAtStart;

    private int _storedCash;
    private Coroutine _coroutine;

    private void Start()
    {
        if (buildAtStart)
            Build();

        cashCounter.text = (cost - _storedCash).ToString();
    }

    protected override void Activate(Transform obj)
    {
        StartCoroutine(GetCash());
    }

    protected override void Deactivate(Transform obj)
    {
        StopAllCoroutines();
    }

    private IEnumerator GetCash()
    {
        while (_storedCash < cost) {
            if (GlobalValueHandler.Cash > 0) {
                int cashSpend = Mathf.Min(new int[] { cashSpendCount, cost - _storedCash, GlobalValueHandler.Cash });

                GlobalValueHandler.Cash -= cashSpend;
                _storedCash += cashSpend;

                cashCounter.text = (cost - _storedCash).ToString();
            }

            yield return new WaitForSeconds(cashSpendDelay);
        }

        Build();
    }

    private void Build()
    {
        foreach (var obj in buildParts)
            obj.gameObject.SetActive(true);

        OrdersManager.AddProductType(allowedType);

        Destroy(transform.parent.gameObject);
    }
}
