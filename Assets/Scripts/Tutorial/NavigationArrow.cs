using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationArrow : MonoBehaviour
{
    [SerializeField] Transform target;

    private void LateUpdate()
    {
        if (!target)
            return;

        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void SetTarget(Transform target)
    {
        if (!target) {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);
        this.target = target;
    }
}
