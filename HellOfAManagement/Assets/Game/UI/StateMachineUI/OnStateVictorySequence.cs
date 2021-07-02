using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HOM
{
    public class OnStateVictorySequence : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            GUIHandler.ActivatesMenu("VictoryScreen");
            Time.timeScale = 0;
            Debug.Log("Won");
        }
    }
}
