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
    [SerializeField] private Button continueButton;

    private string _savePath;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var savePath = Path.Combine(Application.persistentDataPath, GameLoader.FileName);
#else
        _savePath = Path.Combine(Application.dataPath, GameLoader.FileName);
#endif

        continueButton.interactable = File.Exists(_savePath);
    }

    public void StartNewGame()
    {
        if(File.Exists(_savePath))
        {
            foreach (var item in startNewGameAnimator)
            {
                item.SetBool("StartAnimation", true);
            }
        }
        else
        {
            GameLoader.startNewGame = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void ContinueGame()
    {
        GameLoader.startNewGame = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
