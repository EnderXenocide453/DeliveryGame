using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
public class CouriersUI : MonoBehaviour
{
    [SerializeField] private GameObject hireCouriersPanel;
    [SerializeField] private GameObject contentField;
    [Space]
    [SerializeField] private GameObject substratePrefab;
    [SerializeField] private CourierManager courierManager;
    public void OnCourierControllerExitButtonClick()
    {
        Time.timeScale = 1;
        hireCouriersPanel.gameObject.SetActive(false);
    }
    public void OnCourierControllerButtonClick()
    {
        Time.timeScale = 0;
        hireCouriersPanel.gameObject.SetActive(true);
    }
    public void OnHireButtonClick()
    {
        GameObject instantiatedPrefab = Instantiate(substratePrefab, contentField.transform);
        RectTransform prefabTransform = instantiatedPrefab.GetComponent<RectTransform>();

        //Vector3 newPosition = original.rectTransform.position;
        //newPosition.y = _currentYPosition;
        //prefabTransform.position = newPosition;

        //_currentYPosition -= _distanceBetweenButtons;

        courierManager.SpawnCourier();
    }
    //private void PlaceButtonUnderSububstrate(Vector3 substratePosition)
    //{
    //    Vector3 newPosition = substratePosition;
    //    newPosition.y -= _distanceBetweenButtons;
    //    hireNewCourierButton.transform.position = newPosition;
    //}
}
