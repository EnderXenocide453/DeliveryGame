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
        if (_attachedUpgrade == null)
            return;

        nameField.text = _attachedUpgrade.name;
        descField.text = _attachedUpgrade.description;
        costField.text = _attachedUpgrade.cost.ToString();

        UpdateButton();
    }

    public void DoUpgrade()
    {
        _attachedUpgrade?.DoUpgrade();
    }

    private void UpdateButton()
    {
        upgradeBtn.interactable = GlobalValueHandler.Cash >= _attachedUpgrade.cost;
    }

    private void OnUpgrade()
    {
        if (_attachedUpgrade.nextUpgrade == null) {
            Destroy(gameObject);

            return;
        }

        AttachUpgrade(_attachedUpgrade.nextUpgrade);
    }
}
