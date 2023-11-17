using UnityEngine;
using UnityEngine.EventSystems;

public class SwitchButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Transform[] firstGroup;
    [SerializeField] Transform[] secondGroup;

    private bool isSwitched;

    private void Start()
    {
        foreach (var obj in firstGroup)
            obj.gameObject.SetActive(true);

        foreach (var obj in secondGroup)
            obj.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var obj in firstGroup)
            obj.gameObject.SetActive(isSwitched);

        isSwitched = !isSwitched;

        foreach (var obj in secondGroup)
            obj.gameObject.SetActive(isSwitched);
    }
}
