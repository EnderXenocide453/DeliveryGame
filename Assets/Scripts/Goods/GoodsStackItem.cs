using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsStackItem : MonoBehaviour
{
    [SerializeField] Transform Connector;

    private int _id;
    private GoodsStackItem _connectedItem;
    private GoodsStackItem _parent;
    
    public int ID
    {
        get => _id;
        set
        {
            Debug.Log("ID changed");
            _id = value;
            if (_connectedItem)
                _connectedItem.ID = value + 1;
        }
    }

    private void OnDestroy()
    {
        Disconnect();
    }

    public void ConnectTo(GoodsStackItem parent)
    {
        if (!parent) {
            Debug.Log("parent is null");
            ID = 0;
            transform.localPosition = Vector3.zero;
            
            return;
        }

        _parent = parent;
        parent._connectedItem = this;

        ID = parent.ID + 1;

        transform.position = parent.Connector.position;
    }

    public void Disconnect()
    {
        if (_connectedItem)
            _connectedItem.ConnectTo(_parent);

        _connectedItem = null;
        _parent = null;
        //transform.parent = null;
    }
}
