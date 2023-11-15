using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    [SerializeField] GameObject QueuePointPrefab;

    [SerializeField] int QueueLength = 10;
    [SerializeField] float QueueInterval = 0.25f;

    public Transform[] QueuePoints { get; private set; }

    private void Awake()
    {
        CreateQueue();
    }

    public void CreateQueue()
    {
        if (QueueLength <= 0)
            return;

        if (QueuePoints != null) {
            foreach (var point in QueuePoints)
                if (point)
                    DestroyImmediate(point.gameObject);
        }

        QueuePoints = new Transform[QueueLength];
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < QueueLength; i++) {
            QueuePoints[i] = Instantiate(QueuePointPrefab, transform).transform;
            QueuePoints[i].localPosition = pos;

            pos += Vector3.forward * QueueInterval;
        }
    }
}
