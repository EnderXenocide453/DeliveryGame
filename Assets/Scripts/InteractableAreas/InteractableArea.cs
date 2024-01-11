using UnityEngine;

public abstract class InteractableArea : TutorialObject
{
    [SerializeField] LayerMask InteractionMask;
    [SerializeField] float activeAreaScale = 1.5f;

    private Vector3 _initialScale;

    private void Awake()
    {
        _initialScale = transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0 && other.TryGetComponent<Interactor>(out var interactor)) {
            interactor.AddArea(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & InteractionMask.value) > 0 && other.TryGetComponent<Interactor>(out var interactor)) {
            Deactivate(other.transform);

            interactor.RemoveArea(this);
        }
    }

    public void Activate(Transform obj)
    {
        transform.localScale = _initialScale * activeAreaScale;
        OnActivate(obj);
    }

    public void Deactivate(Transform obj)
    {
        transform.localScale = _initialScale;
        OnDeactivate(obj);
    }

    protected abstract void OnActivate(Transform obj);
    protected abstract void OnDeactivate(Transform obj);
}
