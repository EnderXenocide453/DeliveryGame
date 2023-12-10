using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [SerializeField] AnimationCurve floatingCurve;
    [SerializeField, Range(0, 1)] float currentState;
    [SerializeField] float speed = 1;
    [SerializeField] float strength = 1;

    private void OnDrawGizmos()
    {
        Draw();
    }

    private void Update()
    {
        currentState = (currentState + Time.deltaTime * speed) % 1;
        Draw();
    }

    private void Draw()
    {
        transform.localPosition = Vector3.up * strength * floatingCurve.Evaluate(currentState);
    }
}
