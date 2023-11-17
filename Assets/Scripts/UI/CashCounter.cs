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
    }

    private void UpdateCounter()
    {
        Debug.Log("�");
        _text.text = GlobalValueHandler.Cash.ToString();
    }
}
