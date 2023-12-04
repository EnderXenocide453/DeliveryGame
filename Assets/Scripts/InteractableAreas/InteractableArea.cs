using UnityEngine;

public abstract class InteractableArea : MonoBehaviour
{
    [SerializeField] private LayerMask InteractionMask;

    public delegate void InteractableAreaEventHandler();
    public event InteractableAreaEventHandler onActivated;
    public event InteractableAreaEventHandler onDeactivated;

    private PlayerInteraction _interaction;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<PlayerInteraction>(out var player))
        {
            //onActivated?.Invoke();
            //Activate(other.transform);
            _interaction = player;
            player.AddArea(this);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0 && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent<PlayerInteraction>(out var player))
        {
            //onDeactivated?.Invoke();
            Deactivate(other.transform);

            player.RemoveArea(this);
        }
    }

    private void OnDisable()
    {
        _interaction?.RemoveArea(this);
    }

    private void OnDestroy()
    {
        _interaction?.RemoveArea(this);
    }

    public abstract void Activate(Transform obj);
    public abstract void Deactivate(Transform obj);
}
