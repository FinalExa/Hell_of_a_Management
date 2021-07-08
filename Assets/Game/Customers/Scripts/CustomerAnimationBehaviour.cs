using UnityEngine;

public class CustomerAnimationBehaviour : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Release", true);
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
