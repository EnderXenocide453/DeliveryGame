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
    private string _savePath;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _savePath = Path.Combine(Application.persistentDataPath, GameLoader.FileName);
#else
        _savePath = Path.Combine(Application.dataPath, GameLoader.FileName);
#endif

        resumeButton.interactable = File.Exists(_savePath);
    }

    public void StartNewGame()
    {
        GameLoader.startNewGame = true;

        if (!File.Exists(_savePath) || warning.gameObject.activeInHierarchy) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        warning.gameObject.SetActive(true);
    }

    public void ContinueGame()
    {
        GameLoader.startNewGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
