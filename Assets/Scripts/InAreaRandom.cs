using System.Collections;
using UnityEngine;

[System.Serializable]
public class InAreaRandom
{
    [SerializeField] Area[] areas;

    public Vector3 GetRandomPoint()
    {
        if (areas.Length == 0)
            return Vector3.zero;

        return areas[Random.Range(0, areas.Length - 1)].GetRandomPoint();
    }

    /// <summary>
    /// Выпуклая форма
    /// </summary>
    [System.Serializable]
    private struct Area
    {
        public Transform[] anchors;

        public Vector3 GetRandomPoint()
        {
            if (anchors.Length == 0)
                return Vector3.zero;

            if (anchors.Length == 1)
                return anchors[0].position;

            Vector3 posA = Vector3.Lerp(GetRandomAnchor(), GetRandomAnchor(), Random.Range(0f, 1f));
            Vector3 posB = Vector3.Lerp(GetRandomAnchor(), GetRandomAnchor(), Random.Range(0f, 1f));

            return Vector3.Lerp(posA, posB, Random.Range(0f, 1f));
        }

        public Vector3 GetRandomAnchor()
        {
            return anchors[Random.Range(0, anchors.Length - 1)].position;
        }
    }
}
