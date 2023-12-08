using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AddMoney : MonoBehaviour
{
    [SerializeField] private GameObject addMoneyMenu;
    [SerializeField] private Button moneyForAdButton;

    [SerializeField] private PlayerMovement movement;
    
    [SerializeField] private VideoPlayer adVideo;
    [SerializeField] private float adDuration = 30;
    [SerializeField] private GameObject adCanvas;
    private void Start()
    {
        addMoneyMenu.SetActive(false);
        adCanvas.SetActive(false);
    }
    private IEnumerator StopAdvertisement()
    {
        yield return new WaitForSeconds(adDuration);
        adVideo.Stop();
        adCanvas.SetActive(false);
    }

    public void OnClick()
    {
        addMoneyMenu.SetActive(true);
        movement.ChangeMove(false);
        moneyForAdButton.interactable = true;
    }

    public void ExitClick()
    {
        addMoneyMenu.SetActive(false);
        movement.ChangeMove(true);
        StopCoroutine(StopAdvertisement());
    }

    public void OneHundredBucksClick()
    {
        adCanvas.SetActive(true);
        adVideo.Play();
        StartCoroutine(StopAdvertisement());
        moneyForAdButton.interactable = false;
        GlobalValueHandler.Cash += 100;
    }
}
