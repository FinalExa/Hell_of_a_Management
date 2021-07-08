using UnityEngine;

public class Animations : MonoBehaviour
{
    public Animator animator;
    [HideInInspector] public bool waitForAnimation;
    [SerializeField] protected StateMachine stateMachineToRead;
    protected string actualState;

    public virtual void AnimatorStateUpdate(string statePassed)
    {
        if (animator != null)
        {
            if (!string.IsNullOrEmpty(actualState)) animator.SetBool(actualState, false);
            SetupStateBool(statePassed);
        }
    }
    public virtual void SetupStateBool(string statePassed)
    {
        actualState = statePassed;
        animator.SetBool(actualState, true);
    }
    public virtual void AnimationIsOver()
    {
        waitForAnimation = false;
    }
}
