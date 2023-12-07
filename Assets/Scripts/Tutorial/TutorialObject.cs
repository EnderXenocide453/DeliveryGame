public abstract class TutorialObject
{
    public bool activeTutorial = false;

    public void EndStep()
    {
        if (activeTutorial)
            TutorialManager.instance.NextStep();
    }
}
