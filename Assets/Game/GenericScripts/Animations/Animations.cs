using UnityEngine;

public abstract class Animations : MonoBehaviour
{
    [HideInInspector] public bool waitForAnimation;
    [SerializeField] protected Animator animator;
    [SerializeField] protected StateMachine stateMachineToRead;
    [SerializeField] private string[] statesToExclude;
    protected string actualState;

    public virtual void AnimatorStateUpdate(string statePassed)
    {
        if (!string.IsNullOrEmpty(actualState)) animator.SetBool(actualState, false);
        SetupStateBool(statePassed);
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
