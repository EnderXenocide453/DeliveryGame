using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void Update()
    {
        LookAtCamera();
    }

    private void LookAtCamera()
    {
        Vector3 target = transform.position + transform.position - Camera.main.transform.position;
        target.x = transform.position.x;

        transform.LookAt(target);
    }
}
