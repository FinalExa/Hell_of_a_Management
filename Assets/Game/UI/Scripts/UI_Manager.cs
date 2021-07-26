using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    public class UI_Manager : MonoBehaviour
    {
        public static UI_Manager uiManagerInstance;
        Animator animator;

        #region UnityCallbacks

        void Start()
        {
            Init();
            DontDestroyOnLoad(this);
        }

        void Update()
        {
            CheckStatus();
        }

        #endregion

        void Init()
        {
            DontDestroyOnLoad(this);

            if (uiManagerInstance == null)
                uiManagerInstance = this;
            else
                Destroy(gameObject);

            animator = gameObject.GetComponent<Animator>();
            animator.SetInteger("Index", LevelManager.levelManagerInstance.currentIndex);
        }

        /// <summary>
        /// This is used to check if the game is paused or resumed.
        /// </summary>
        void CheckStatus()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !LevelManager.levelManagerInstance.isLoading)
            {
                if (animator.GetBool("Paused"))
                {
                    animator.SetBool("Paused", false);
                }
                else if (!animator.GetBool("Paused"))
                {
                    animator.SetBool("Paused", true);
                }
                PauseGame();
            }
        }

        void PauseGame()
        {
            if (animator.GetBool("Paused"))
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }