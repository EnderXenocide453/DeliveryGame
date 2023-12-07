using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;

    [SerializeField] MessageWindow messageWindow;
    [SerializeField] NavigationArrow navArrow;
    [SerializeField] TipArrow tipArrow;
    [Space]
    [SerializeField] TutorialStep[] steps;
    private int _currentStep = 0;

    private void Awake()
    {
        if (instance) {
            Destroy(this);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        if (GameLoader.startNewGame)
            ActivateStep(0);
    }

    public void NextStep()
    {
        if (_currentStep >= steps.Length - 1)
            return;

        _currentStep++;

        if (_currentStep > 0)
            DeactivateStep(_currentStep - 1);

        ActivateStep(_currentStep);
    }

    private void ActivateStep(int id)
    {
        messageWindow.SetText(steps[id].text);
        navArrow.SetTarget(steps[id].navigationTarget);
        tipArrow.SetTarget(steps[id].tipTarget);

        foreach (var btn in steps[id].disabledButtons) {
            btn.interactable = false;
        }

        foreach (var transform in steps[id].disabledObjects) {
            transform.gameObject.SetActive(false);
        }

        foreach (var transform in steps[id].enabledObjects) {
            transform.gameObject.SetActive(true);
        }

        foreach (var behaviour in steps[id].disabledBehaviours) {
            behaviour.enabled = false;
        }

        foreach (var behaviour in steps[id].enabledBehaviours) {
            behaviour.enabled = true;
        }

        foreach (var obj in steps[id].tutorialObjects) {
            obj.activeTutorial = true;
        }
    }

    private void DeactivateStep(int id)
    {
        foreach (var btn in steps[id].disabledButtons) {
            btn.interactable = true;
        }

        foreach (var transform in steps[id].disabledObjects) {
            transform.gameObject.SetActive(true);
        }

        foreach (var transform in steps[id].enabledObjects) {
            transform.gameObject.SetActive(false);
        }

        foreach (var behaviour in steps[id].disabledBehaviours) {
            behaviour.enabled = true;
        }

        foreach (var behaviour in steps[id].enabledBehaviours) {
            behaviour.enabled = false;
        }

        foreach (var obj in steps[id].tutorialObjects) {
            obj.activeTutorial = false;
        }
    }

    [System.Serializable]
    public struct TutorialStep
    {
        [TextArea] public string text;
        public Transform navigationTarget;
        public Transform tipTarget;
        public TutorialObject[] tutorialObjects;
        public Button[] disabledButtons;
        public Transform[] disabledObjects;
        public Transform[] enabledObjects;
        public Behaviour[] disabledBehaviours;
        public Behaviour[] enabledBehaviours;
    }
}
