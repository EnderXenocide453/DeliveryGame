using TMPro;
using UnityEngine;

public class StrorageCountVisualizer : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private TMP_Text counter;
    
    private void Update()
    {
        counter.text = $"{storage.CurrentCount}/{storage.MaxCount}";
    }
}
