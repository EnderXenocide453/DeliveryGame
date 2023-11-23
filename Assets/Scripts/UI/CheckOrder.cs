using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CheckOrder : MonoBehaviour
{
    [SerializeField] private float timeToTurnOff;
    [Space]
    [SerializeField] private Image image;
    [Space]
    [SerializeField] private Sprite newSprite;


    private Transform _lookAtTransform;
    private void Awake()
    {
        _lookAtTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    private void Start()
    {
        image.gameObject.SetActive(false);
    }

    public void TimeOfAnnotation()
    {
        image.gameObject.SetActive(true);
        image.overrideSprite = newSprite;
        StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(timeToTurnOff);
        image.gameObject.SetActive(false);
    } 
}

[CustomEditor(typeof(CheckOrder))]
public class MyScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Change image"))
        {
            CheckOrder checkScript = (CheckOrder)target;
            checkScript.TimeOfAnnotation();
        }
    }
}