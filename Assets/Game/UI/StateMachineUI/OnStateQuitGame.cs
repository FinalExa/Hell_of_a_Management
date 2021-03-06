using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStateQuitGame : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.ActivatesMenu("Quit Menu");
        }

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.DeactivatesMenu("Quit Menu");
        }
    }
}
