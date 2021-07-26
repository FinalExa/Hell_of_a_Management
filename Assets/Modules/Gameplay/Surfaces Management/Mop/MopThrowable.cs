using System;
using UnityEngine;
public class MopThrowable : ThrowableObject
{
    public static Action continueTutorial;
    [SerializeField] private bool isTutorial;
    private bool tutorialHintGiven;
    private void Update()
    {
        if (isTutorial && !tutorialHintGiven && IsAttachedToHand)
        {
            continueTutorial();
            tutorialHintGiven = true;
        }
    }
}
