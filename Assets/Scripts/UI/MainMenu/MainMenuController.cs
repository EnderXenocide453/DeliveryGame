using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Animator[] startNewGameAnimator;
    [SerializeField] private GameObject warning;
    [SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Button resumeButton;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var savePath = Path.Combine(Application.persistentDataPath, GameLoader.FileName);
#else
        var savePath = Path.Combine(Application.dataPath, GameLoader.FileName);
#endif

        resumeButton.interactable = File.Exists(savePath);
    }

    public void StartNewGame()
    {
        foreach (var item in startNewGameAnimator)
        {
            item.SetBool("StartAnimation", true);
        }
    }

    public void ContinueGame()
    {
        GameLoader.startNewGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
