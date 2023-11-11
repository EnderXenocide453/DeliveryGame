using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    private float _dirX, _dirZ;

    [SerializeField] private Joystick joystick;

    private Rigidbody _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _dirX = joystick.Horizontal * speed;
        _dirZ = joystick.Vertical * speed;

        // Определение направления вращения вокруг вертикальной оси (ось Y)
        Vector3 joystickDirection = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);
        if (joystickDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(joystickDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * 500f);
        }
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(_dirX, _rb.velocity.y, _dirZ);
        _rb.velocity = move * speed * Time.fixedDeltaTime;
    }
}
