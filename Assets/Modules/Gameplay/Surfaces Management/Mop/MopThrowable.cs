using UnityEngine;
public class MopThrowable : ThrowableObject
{
    [SerializeField] private bool isTutorial;
    private bool tutorialHintGiven;
    private void Update()
    {
        if (isTutorial && !tutorialHintGiven && IsAttachedToHand)
        {
            Tutorial.instance.ShowTutorialScreen();
            tutorialHintGiven = true;
        }
    }
}
