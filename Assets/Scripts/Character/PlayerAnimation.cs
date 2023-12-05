using UnityEngine;
[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    private PlayerMovement _movement;
    private Storage _storage;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _anim = GetComponent<Animator>();
        _storage = GetComponent<Storage>();

        _storage.onCountChanged += OnGoodsChanged;
    }

    private void Update()
    {
        WalkAnimation();
    }

    private void WalkAnimation()
    {
        _anim.SetBool("IsMoving",_movement.canMove && _movement.moveDir.magnitude > 0.1f);
    }

    private void OnGoodsChanged()
    {
        _anim.SetBool("WithFood", !_storage.Empty);
    }
}
