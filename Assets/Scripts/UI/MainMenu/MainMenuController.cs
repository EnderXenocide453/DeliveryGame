using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Animator[] startNewGameAnimator;
    [SerializeField] private GameObject warning;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource audioSource;

    public void StartNewGame()
    {
        foreach (var item in startNewGameAnimator)
        {
            item.SetBool("StartAnimation", true);
        }
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
