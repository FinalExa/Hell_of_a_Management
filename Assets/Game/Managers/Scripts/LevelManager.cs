using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager levelManagerInstance;
        public GameObject loadingScreen;
        public bool isLoading;
        public float loadingDuration;
        public int currentIndex;
        Animator ui_Animator;

        #region UnityCallbacks
        private void Awake()
        {
            Init();
        }
        #endregion

        void Init()
        {
            DontDestroyOnLoad(this);

            if (levelManagerInstance == null)
            levelManagerInstance = this;
            else
                DestroyObject(gameObject);

            ui_Animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
        }

        public static void LoadLevel(string levelName)
        {
            switch(levelName)
            {
                case "Main Menu":
                levelManagerInstance.StartCoroutine(levelManagerInstance.ExecuteLevelTransition(levelManagerInstance.loadingDuration, 0));
                    break;
                case "Level":
                levelManagerInstance.StartCoroutine(levelManagerInstance.ExecuteLevelTransition(levelManagerInstance.loadingDuration, 1));
                    break;
            }    
        }

        public void OnLevelWasLoaded(int level)
        {
            if (level == 0)
            {
                ui_Animator.SetInteger("Index", 0);
                currentIndex = 0;
            }
            else
            {
                ui_Animator.SetInteger("Index", 1);
                currentIndex = 1;
            }
        }

        IEnumerator ExecuteLevelTransition(float duration, int levelIndex)
        {
            loadingScreen.SetActive(true);
            isLoading = true;
            yield return new WaitForSeconds(1);
            yield return new WaitForSeconds(duration);
            SceneManager.LoadScene(levelIndex);
            loadingScreen.GetComponent<Animator>().SetTrigger("Out");
            yield return new WaitForSeconds(1);
            loadingScreen.SetActive(false);
            isLoading = false;
            ui_Animator.SetTrigger("SceneChanged");
        }
    }
