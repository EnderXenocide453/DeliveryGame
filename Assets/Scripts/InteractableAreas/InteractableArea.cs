using UnityEngine;

public abstract class InteractableArea : MonoBehaviour
{
    [SerializeField] private LayerMask InteractionMask;

    public delegate void InteractableAreaEventHandler();
    public event InteractableAreaEventHandler onActivated;
    public event InteractableAreaEventHandler onDeactivated;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0) {
            onActivated?.Invoke();
            Activate(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0) {
            onDeactivated?.Invoke();
            Deactivate(other.transform);
        }
    }

    protected abstract void Activate(Transform obj);
    protected abstract void Deactivate(Transform obj);
}
