using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TMP_Text nameField;
    [SerializeField] TMP_Text descField;
    [SerializeField] TMP_Text buttonTextField;
    [SerializeField] Button upgradeButton;

    private UpgradeQueue _attachedUpgradeQueue;

    public void AttachUpgrade(UpgradeQueue upgrade)
    {
        _attachedUpgradeQueue = upgrade;
        _attachedUpgradeQueue.onUpgraded += OnUpgrade;

        _attachedUpgradeQueue.onLocked += LockUpgrade;
        _attachedUpgradeQueue.onUnlocked += UnlockUpgrade;

        GlobalValueHandler.onCashChanged += UpdateButton;

        DrawUpgrade();
    }

    public void DrawUpgrade()
    {
        if (_attachedUpgradeQueue == null) {
            buttonTextField.text = "Max";
            upgradeButton.interactable = false;

            GlobalValueHandler.onCashChanged -= UpdateButton;

            return;
        }

        if (_attachedUpgradeQueue.isLocked) {
            LockUpgrade();
            return;
        }

        nameField.text = _attachedUpgradeQueue.CurrentUpgrade.Name;
        descField.text = _attachedUpgradeQueue.CurrentUpgrade.Description;
        buttonTextField.text = _attachedUpgradeQueue.CurrentUpgrade.cost.ToString();

        UpdateButton();
    }

    public void DoUpgrade()
    {
        GlobalValueHandler.Cash -= _attachedUpgradeQueue.CurrentUpgrade.cost;
        _attachedUpgradeQueue?.CurrentUpgrade?.DoUpgrade();
    }

    private void UpdateButton()
    {
        if (!_attachedUpgradeQueue.isLocked)
            upgradeButton.interactable = GlobalValueHandler.Cash >= _attachedUpgradeQueue?.CurrentUpgrade?.cost;
    }

    private void OnUpgrade()
    {
        if (_attachedUpgradeQueue.CurrentUpgrade == null) {
            OnMaxUpgrade();

            return;
        }

        DrawUpgrade();
    }

    private void LockUpgrade()
    {
        descField.text = "Пока недоступно";
        upgradeButton.gameObject.SetActive(false);
    }

    private void UnlockUpgrade()
    {
        upgradeButton.gameObject.SetActive(true);
        DrawUpgrade();
    }

    private void OnMaxUpgrade()
    {
        _attachedUpgradeQueue = null;

        DrawUpgrade();
    }
}
