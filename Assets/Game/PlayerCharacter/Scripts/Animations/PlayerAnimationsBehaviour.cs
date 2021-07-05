using System;
using UnityEngine;

public class PlayerAnimationsBehaviour : StateMachineBehaviour
{
    public static Action onAnimationEnd;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        onAnimationEnd();
    }
}
