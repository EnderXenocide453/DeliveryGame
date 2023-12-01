using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform first;
    [SerializeField] Transform second;

    private void Start()
    {
        //first.gameObject.SetActive(true);
        //second.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        first.gameObject.SetActive(second.gameObject.activeSelf);
        second.gameObject.SetActive(!second.gameObject.activeSelf);
    }
}
