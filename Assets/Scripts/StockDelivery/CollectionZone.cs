using UnityEngine;

public class CollectionZone : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float distanceFromPlayer = 1;
    private void Update()
    {
        transform.position = playerTransform.position + playerTransform.forward * distanceFromPlayer;
    }
}
