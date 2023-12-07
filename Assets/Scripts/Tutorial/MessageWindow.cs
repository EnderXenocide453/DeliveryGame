using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MessageWindow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_Text textField;

    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }

    public void SetText(string text)
    {
        if (text.Length == 0)
            return;

        gameObject.SetActive(true);
        textField.text = text;
    }
}
