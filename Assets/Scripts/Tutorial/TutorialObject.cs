using UnityEngine;

public abstract class TutorialObject : MonoBehaviour
{
    public bool activeTutorial = false;

    public virtual void EndStep()
    {
        if (activeTutorial)
            TutorialManager.instance.NextStep();
    }
}
