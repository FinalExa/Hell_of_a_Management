using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStateEndGame : StateMachineBehaviour
    {
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Time.timeScale = 1;
            animator.SetBool("Paused", false);
            AudioManager.instance.StopAllSounds();
            GUIHandler.DeactivatesMenu("Pause Menu");
        }
    }
}
