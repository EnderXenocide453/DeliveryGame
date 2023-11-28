using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] Sprite enabledSprite;
    [SerializeField] Sprite disabledSprite;

    private Toggle _toggle;
    private Image _image;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
        _image = GetComponent<Image>();

        _toggle.onValueChanged.AddListener(OnSwitch);

        if (_toggle.isOn)
            OnSwitch(true);
        else
            OnSwitch(false);
    }

    private void OnSwitch(bool on)
    {
        if (on) {
            _image.sprite = enabledSprite;
        } else {
            _image.sprite = disabledSprite;
        }
    }
}
