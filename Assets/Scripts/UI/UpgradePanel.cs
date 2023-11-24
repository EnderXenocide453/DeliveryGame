using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TMP_Text nameField;
    [SerializeField] TMP_Text descField;
    [SerializeField] TMP_Text costField;
    [SerializeField] Button upgradeBtn;

    private BaseUpgrade _attachedUpgrade;

    public void AttachUpgrade(BaseUpgrade upgrade)
    {
        _attachedUpgrade = upgrade;
        _attachedUpgrade.onUpgraded += OnUpgrade;

        GlobalValueHandler.onCashChanged += UpdateButton;

        DrawUpgrade();
    }

    public void DrawUpgrade()
    {
        if (_attachedUpgrade == null) {
            descField.text = "";
            costField.text = "Max";
            upgradeBtn.interactable = false;

            GlobalValueHandler.onCashChanged -= UpdateButton;

            return;
        }

        nameField.text = _attachedUpgrade.Name;
        descField.text = _attachedUpgrade.Description;
        costField.text = _attachedUpgrade.cost.ToString();

        UpdateButton();
    }

    public void DoUpgrade()
    {
        GlobalValueHandler.Cash -= _attachedUpgrade.cost;
        _attachedUpgrade?.DoUpgrade();
    }

    private void UpdateButton()
    {
        upgradeBtn.interactable = GlobalValueHandler.Cash >= _attachedUpgrade?.cost;
    }

    private void OnUpgrade()
    {
        if (_attachedUpgrade.nextUpgrade == null) {
            OnMaxUpgrade();

            return;
        }

        AttachUpgrade(_attachedUpgrade.nextUpgrade);
    }

    private void LockUpgrade()
    {

    }

    private void UnlockUpgrade()
    {

    }

    private void OnMaxUpgrade()
    {
        _attachedUpgrade = null;

        DrawUpgrade();
    }
}
