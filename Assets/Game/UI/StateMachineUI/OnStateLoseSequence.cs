using UnityEngine;

namespace HOM
{
    public class OnStateLoseSequence : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.ActivatesMenu("LoseScreen");
            Time.timeScale = 0;
        }
    }
}
