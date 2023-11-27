using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private bool isFood;

    private Animator _anime;
    private PlayerMovement _movement;
    private void Awake()
    {
        _movement = GetComponentInParent<PlayerMovement>();
        _anime = GetComponentInParent<Animator>();
    }
    private void Update()
    {
        WalkAnimation();
        TransportFood();
    }
    private void WalkAnimation()
    {
        _anime.SetBool("IsMoving", _movement.moveDir.magnitude > 0.1f);
    }
    private void TransportFood()
    {
        if (isFood)
        {
            _anime.SetBool("IsFood", true);
        }
        else
        {
            _anime.SetBool("IsFood", false);
        }
    }
}
