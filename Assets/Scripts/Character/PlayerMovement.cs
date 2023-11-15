using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    [SerializeField] private float RotationSpeed = 360;
    private Vector3 _moveDir;

    [SerializeField] private Joystick joystick;

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _moveDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if (_moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(_moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = _moveDir * speed * Time.fixedDeltaTime;
    }
}
