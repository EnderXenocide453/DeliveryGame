using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndStepButton : MonoBehaviour, IPointerClickHandler
{
    private TutorialManager _manager;

    private void Awake()
    {
        enabled = false;
        _manager = FindObjectOfType<TutorialManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (enabled)
            _manager.NextStep();
    }
}
