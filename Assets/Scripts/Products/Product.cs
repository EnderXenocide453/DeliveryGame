using UnityEngine;
public class Product : MonoBehaviour
{
    [SerializeField] private Stash[] stashTypes;
    [Space]
    [SerializeField] private CharacterController characterController;

    public string productType; //[HideInInspector]

    private void Update()
    {
        foreach (var stashType in stashTypes)
        {
            if (stashType.stashType.ToString().ToLower() == productType.ToLower())
            {
                characterController.stash = stashType;
            }
        }
    }
}