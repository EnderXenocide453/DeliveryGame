using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuildArea : InteractableArea
{
    [SerializeField] int cost;
    [SerializeField] Transform[] buildParts;
    [SerializeField] float cashSpendDelay;
    [SerializeField] TMPro.TMP_Text cashCounter;
    [Space]
    [SerializeField] UnityEvent onBuildEvent;
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
                GlobalValueHandler.Cash--;
                _storedCash++;

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

        onBuildEvent?.Invoke();

        Destroy(transform.parent.gameObject);
    }
}
