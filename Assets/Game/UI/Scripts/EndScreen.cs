using UnityEngine;
public class EndScreen : MonoBehaviour
{
    Animator animator;
    public void Start()
    {
        animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
    }

    public void Restart()
    {
        animator.SetTrigger("EndGame");
        LevelManager.LoadLevel("Level");
    }

    public void GoToMainMenu()
    {
        AudioManager.instance.StopAllSounds();
        animator.SetTrigger("EndGame");
        LevelManager.LoadLevel("Main Menu");
    }
}
