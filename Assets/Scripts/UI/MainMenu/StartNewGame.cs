using UnityEngine;

public class StartNewGame : MonoBehaviour
{
    [SerializeField] private Animator[] startNewGameAnimator;
    [SerializeField] private GameObject warning;

    public void OnClick()
    {
        foreach (var item in startNewGameAnimator)
        {
            item.SetBool("StartAnimation", true); 
        }

    }
}
