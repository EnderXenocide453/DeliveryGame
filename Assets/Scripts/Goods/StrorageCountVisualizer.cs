using TMPro;
using UnityEngine;

public class StrorageCountVisualizer : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private TextMeshPro weHave;
    
    private void Update()
    {
        weHave.text = $"{storage.CurrentCount.ToString()}/{storage.MaxCount.ToString()}";
    }
}
