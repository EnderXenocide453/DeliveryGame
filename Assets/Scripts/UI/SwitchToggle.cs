using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform handle;
    [SerializeField] Color enabledColor;
    [SerializeField] Color disabledColor;

    private Toggle _toggle;
    private Image _image;
    Vector2 _handlePos;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _image = GetComponent<Image>();

        _handlePos = handle.anchoredPosition;
        _toggle.onValueChanged.AddListener(OnSwitch);

        if (_toggle.isOn)
            OnSwitch(true);
        else
            OnSwitch(false);
    }

    private void OnSwitch(bool on)
    {
        if (on) {
            handle.anchoredPosition = -_handlePos;
            _image.color = enabledColor;
        } else {
            handle.anchoredPosition = _handlePos;
            _image.color = disabledColor;
        }
    }
}
