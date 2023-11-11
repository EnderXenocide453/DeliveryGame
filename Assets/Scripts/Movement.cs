using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float deceleration = 5;

    [SerializeField] private float speed = 5;
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
    }
    private void FixedUpdate()
    {
        Vector3 move = new Vector3(_dirX, _rb.velocity.y, _dirZ);
        _rb.velocity = move * speed * Time.fixedDeltaTime;
    }
    //private void Move()
    //{
    //    Vector3 velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.y);
    //    velocity = Vector3.MoveTowards(velocity, )
    //}
}
