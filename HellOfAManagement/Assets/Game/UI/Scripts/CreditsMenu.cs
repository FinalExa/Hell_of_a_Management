using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class CreditsMenu : MonoBehaviour
    {
        Animator animator;

        public void Start()
        {
            animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public void Back()
        {
                animator.SetTrigger("Back");
        }
    }
