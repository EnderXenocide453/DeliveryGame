using UnityEngine;

public abstract class BaseUpgradeUI : MonoBehaviour
{
    [SerializeField] private GameObject contentField;
    [SerializeField] private GameObject upgradeRowPrefab;

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    protected void AddUpgradePanel(UpgradeQueue queue)
    {
        UpgradePanel panel = Addpanel();
        panel.AttachUpgrade(queue);
    }

    protected UpgradePanel Addpanel() => Instantiate(upgradeRowPrefab, contentField.transform).GetComponent<UpgradePanel>();
}
