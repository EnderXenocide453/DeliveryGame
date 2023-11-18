using UnityEngine;
using UnityEngine.UI;

public class CheckOrder : MonoBehaviour
{
    [SerializeField] private Image suitableOrNot;
    [Space]
    [SerializeField] private Sprite newSuitable;
    [SerializeField] private Sprite newNotSuitable;
    [Space]
    [SerializeField] private bool isSuitable;
    private Transform _characterTransform;
    private void Start()
    {
        _characterTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Update()
    {
        suitableOrNot.transform.LookAt(_characterTransform);
        CheckIsSuitable(isSuitable);
    }
    public void CheckIsSuitable(bool indicator)
    {
        if (indicator)
        {
            suitableOrNot.overrideSprite = newSuitable;
        }
        else
        {
            suitableOrNot.overrideSprite = newNotSuitable;
        }
    }
}
