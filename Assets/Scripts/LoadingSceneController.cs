using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField] RectTransform progressBar;
    [SerializeField] RectTransform progressBarBG;

    void Start()
    {
        StartCoroutine(LoadGameScene());
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation gameScene = SceneManager.LoadSceneAsync(2);

        while (!gameScene.isDone) {
            Debug.Log(gameScene.progress);
            yield return new WaitForEndOfFrame();
            progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, gameScene.progress * progressBarBG.rect.width);
        }
    }
}
