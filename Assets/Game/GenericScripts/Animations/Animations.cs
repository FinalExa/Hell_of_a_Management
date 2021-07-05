using UnityEngine;

public class Animations : MonoBehaviour
{
    [HideInInspector] public bool waitForAnimation;
    [SerializeField] protected Animator animator;
    [SerializeField] protected StateMachine stateMachineToRead;
    [SerializeField] private string[] statesToExclude;
    protected string actualState;

    public virtual void OnEnable()
    {
        if (!string.IsNullOrEmpty(actualState)) SetupStateBool();
    }

    public virtual void UpdateAnimatorValues()
    {
        AnimatorStateUpdate();
    }

    public virtual void AnimatorStateUpdate()
    {
        if (actualState != stateMachineToRead.stateRef && NoStatesToIgnore())
        {
            if (!string.IsNullOrEmpty(actualState)) animator.SetBool(actualState, false);
            SetupStateBool();
        }
    }
    public virtual void SetupStateBool()
    {
        actualState = stateMachineToRead.stateRef;
        animator.SetBool(actualState, true);
    }
    public virtual void AnimationIsOver()
    {
        waitForAnimation = false;
    }
    private bool NoStatesToIgnore()
    {
        bool passed = true;
        for (int i = 0; i < statesToExclude.Length; i++)
        {
            if (stateMachineToRead.stateRef == statesToExclude[i])
            {
                passed = false;
                break;
            }
        }
        return passed;
    }
}
