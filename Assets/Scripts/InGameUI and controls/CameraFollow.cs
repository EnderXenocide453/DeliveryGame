using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [Space]
    [SerializeField] private float smoothSpeed = 0.5f;

    //[SerializeField] private Vector3 offset;
    private void FixedUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        //transform.position = smoothedPosition;
        //transform.LookAt(target);

        transform.position = Vector3.Lerp(transform.position, target.position, smoothSpeed);
    }
}
