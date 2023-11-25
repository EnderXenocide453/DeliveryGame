using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CashCounter : MonoBehaviour
{
    private Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();

        GlobalValueHandler.onCashChanged += UpdateCounter;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _text.text = GlobalValueHandler.Cash.ToString();
    }
}
