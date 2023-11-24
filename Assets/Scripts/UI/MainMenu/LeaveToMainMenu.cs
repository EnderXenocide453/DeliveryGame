using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveToMainMenu : MonoBehaviour
{
    public void OnClick() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
}
