using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EmojiCloud : MonoBehaviour
{
    [SerializeField] private Image image;

    private void Start()
    {
        image.gameObject.SetActive(false);
    }

    public void DrawImage(Sprite sprite, float delay)
    {
        image.sprite = sprite;
        StopAllCoroutines();
        StartCoroutine(ShowCloud(delay));
    }

    public void Clear()
    {
        StopAllCoroutines();
        image.gameObject.SetActive(false);
    }

    private IEnumerator ShowCloud(float delay)
    {
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(delay);
        image.gameObject.SetActive(false);
    } 
}