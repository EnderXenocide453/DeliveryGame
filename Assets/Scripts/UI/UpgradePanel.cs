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
    [SerializeField] Image icon;

    private UpgradeQueue _attachedUpgradeQueue;
    private bool _isMax;

    public void AttachUpgrade(UpgradeQueue upgrade)
    {
        _attachedUpgradeQueue = upgrade;
        _attachedUpgradeQueue.onUpgraded += OnUpgrade;
        _attachedUpgradeQueue.onMaxLevelReached += OnMaxUpgrade;

        _attachedUpgradeQueue.onLocked += LockUpgrade;
        _attachedUpgradeQueue.onUnlocked += UnlockUpgrade;

        GlobalValueHandler.onCashChanged += UpdateButton;

        DrawUpgrade();
    }

    public void DrawUpgrade()
    {
        if (_attachedUpgradeQueue == null) {
            Destroy(gameObject);
            return;
        }

        icon.sprite = _attachedUpgradeQueue.CurrentUIIcon;

        if (_attachedUpgradeQueue.isLocked) {
            LockUpgrade();
            return;
        }

        descField.text = $"{_attachedUpgradeQueue.CurrentUpgrade.Name}\n{_attachedUpgradeQueue.CurrentUpgrade.Description}";

        if (_isMax) { 
            buttonTextField.text = "Макс.";
            upgradeButton.interactable = false;

            GlobalValueHandler.onCashChanged -= UpdateButton;
            return;
        }

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
        DrawUpgrade();
    }

    private void LockUpgrade()
    {
        descField.text = $"{_attachedUpgradeQueue.CurrentUpgrade.Name}\nПока недоступно";
        upgradeButton.gameObject.SetActive(false);
    }

    private void UnlockUpgrade()
    {
        upgradeButton.gameObject.SetActive(true);
        DrawUpgrade();
    }

    private void OnMaxUpgrade()
    {
        _isMax = true; ;

        DrawUpgrade();
    }
}
