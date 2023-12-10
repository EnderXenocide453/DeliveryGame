using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipArrow : MonoBehaviour
{
    private RectTransform _rect;

    private void Start()
    {
        _rect = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    public void SetTarget(RectTransform target)
    {
        if (!target) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
        _rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);

        _rect.SetParent(target);

        _rect.anchoredPosition = Vector2.zero;
        //_rect.position = target.position;
        //_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, target.rect.width);
        //_rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, target.rect.height);
    }
}
