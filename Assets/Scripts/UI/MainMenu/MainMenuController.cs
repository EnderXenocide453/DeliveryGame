using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Animator[] startNewGameAnimator;
    [SerializeField] private GameObject warning;

    public void StartNewGame()
    {
        foreach (var item in startNewGameAnimator)
        {
            item.SetBool("StartAnimation", true);
        }
    }

    public void ResumeGame()
    {
        GameLoader.startNewGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
