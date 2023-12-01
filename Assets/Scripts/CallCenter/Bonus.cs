using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;

    [SerializeField] float bonusAmount;
    [SerializeField] float bonusTime;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<PlayerMovement>(out var player)) {
            player.AddBonus(bonusAmount, bonusTime);

            Destroy(gameObject);
        }
    }
}
