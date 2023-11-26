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

    public int ID;
    public bool alreadyBuilded { get; private set; }
    public int storedCash { get; private set; }

    private Coroutine _coroutine;

    private void Start()
    {
        if (alreadyBuilded)
            Build();

        cashCounter.text = (cost - storedCash).ToString();
    }

    public void Build()
    {
        foreach (var obj in buildParts)
            obj.gameObject.SetActive(true);

        OrdersManager.AddProductType(allowedType);
        alreadyBuilded = true;
        SoundsManager.PlaySound(SoundsManager.instance.buildEndSound);

        transform.parent.gameObject.SetActive(false);
    }

    public void SetCash(int cash)
    {
        storedCash = cash;
        cashCounter.text = (cost - storedCash).ToString();
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
        while (true) {
            if (GlobalValueHandler.Cash > 0) {
                SoundsManager.PlaySound(SoundsManager.instance.buildProgressSound);

                int cashSpend = Mathf.Min(new int[] { cashSpendCount, cost - storedCash, GlobalValueHandler.Cash });

                GlobalValueHandler.Cash -= cashSpend;
                storedCash += cashSpend;

                cashCounter.text = (cost - storedCash).ToString();
            }

            if (storedCash >= cost)
                break;

            yield return new WaitForSeconds(cashSpendDelay);
        }

        Build();
    }
}
