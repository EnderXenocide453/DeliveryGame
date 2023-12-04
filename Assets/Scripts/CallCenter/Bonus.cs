using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] LayerMask playerMask;

    [SerializeField] float bonusAmount;
    [SerializeField] float bonusTime;

    private Collider _collider;

    private void Awake()
    {
        TryGetComponent<Collider>(out _collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<PlayerMovement>(out var player)) {
            player.AddBonus(bonusAmount, bonusTime);

            Destroy(gameObject);
        }
    }

    public void FlyTo(Vector3 pos, float speed, float height)
    {
        StartCoroutine(Fly(pos, speed, height));
    }

    private IEnumerator Fly(Vector3 pos, float speed, float height, float delay = 0.03f)
    {
        Vector3 direction = pos - transform.position;
        float distance = direction.magnitude;
        float curDistance = -1;
        float stepDistance = speed * delay;

        int stepsCount = (int)(distance / stepDistance);

        for (int i = 0; i < stepsCount; i++) {
            yield return new WaitForSeconds(delay);

            curDistance += stepDistance / distance * 2;
            float y = curDistance * curDistance * -height + height;
            transform.position += direction.normalized * stepDistance;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        yield return new WaitForSeconds(delay);
        _collider.enabled = true;
    }
}
