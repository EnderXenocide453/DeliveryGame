using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCourier : MonoBehaviour
{
    [SerializeField] private float Speed = 0.2f;

    public MapPath CourierPath;
    public bool IsAwaits { get; private set; } = true;
    
    private WayPoint _startPoint;
    private bool _isDrag;
    private RectTransform _rectTransform;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (_isDrag) {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rectTransform, Input.mousePosition, Camera.main, out var position);
            
            _rectTransform.position = _rectTransform.TransformPoint(position);
        }
    }

    public void SetStartPoint(WayPoint point)
    {
        _startPoint = point;

        CourierPath = new MapPath();
        CourierPath.TryAddPoint(_startPoint);
    }

    public void StartDrag()
    {
        _isDrag = true;
    }

    public void StopDrag()
    {
        _isDrag = false;
    }

    private void FollowCoursor()
    {

    }
}
