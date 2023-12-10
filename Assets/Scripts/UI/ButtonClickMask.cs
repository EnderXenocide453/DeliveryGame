using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickMask : MonoBehaviour
{
    [Range(0f, 1f)]
    public float AlphaLevel = 1f;
    private Image buttonImage;

    void Start()
    {
        buttonImage = gameObject.GetComponent<Image>();
        buttonImage.alphaHitTestMinimumThreshold = AlphaLevel;
    }
}