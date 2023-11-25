using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndOfAnimation : MonoBehaviour
{
    [SerializeField] private GameObject warning;

    private Button _onClick;
    private void Start()
    {
        _onClick = GetComponent<Button>();
    }
    private void OnAnimationEnd() 
    {
        warning.SetActive(true);
        _onClick.onClick.AddListener(GoToGame);
    }
    private void GoToGame()
    {
        GameLoader.startNewGame = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
}