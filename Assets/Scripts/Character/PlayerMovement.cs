using System.Collections;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 15;
    [SerializeField] private float RotationSpeed = 360;
    [SerializeField] private Joystick joystick;
    [SerializeField] private Timer bonusTimer;

    public float speedModifier = 1;
    public Vector3 moveDir { get; private set; }

    private float _bonusSpeedModifier = 1;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        moveDir = new Vector3(joystick.Horizontal, 0f, joystick.Vertical);

        if (moveDir != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * RotationSpeed);
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = moveDir * speed * (speedModifier * _bonusSpeedModifier) * Time.fixedDeltaTime;
    }

    public void AddBonus(float amount, float activeTime)
    {
        DeativateBonus();
        StartCoroutine(ActivateBonus(amount, activeTime));
        bonusTimer?.StartTimer(activeTime, true, false);
    }

    public void DeativateBonus()
    {
        StopAllCoroutines();
        bonusTimer?.StopTimer();
        _bonusSpeedModifier = 1;
    }

    private IEnumerator ActivateBonus(float amount, float activeTime)
    {
        _bonusSpeedModifier = amount;
        yield return new WaitForSeconds(activeTime);
        _bonusSpeedModifier = 1;
    }
}
