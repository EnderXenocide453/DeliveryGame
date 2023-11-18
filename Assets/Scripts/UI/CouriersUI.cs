using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class CouriersUI : MonoBehaviour
{
    [SerializeField] private GameObject contentField;
    [SerializeField] private GameObject upgradeRowPrefab;

    public void ToggleUI()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
    
    public void OnHireButtonClick()
    {
        Instantiate(upgradeRowPrefab, contentField.transform);

        CourierManager.instance.AddNewCourier();
    }
    //private void PlaceButtonUnderSububstrate(Vector3 substratePosition)
    //{
    //    Vector3 newPosition = substratePosition;
    //    newPosition.y -= _distanceBetweenButtons;
    //    hireNewCourierButton.transform.position = newPosition;
    //}
}
