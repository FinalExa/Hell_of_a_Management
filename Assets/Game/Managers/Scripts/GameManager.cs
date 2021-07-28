using UnityEngine;

public class GameManager : MonoBehaviour
{
    Animator animator;
    Animator ui_animator;
    static GameManager self;

    [SerializeField] LevelData levelsData;

    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        AudioManager.instance.Play("Customer_Enter");
        AudioManager.instance.Play("InGame_Music");
        self = this;
        Timer.OnEndTimer += OnEndTimer;
        animator = gameObject.GetComponent<Animator>();
        ui_animator = GameObject.Find("UI_Manager").GetComponent<Animator>();
    }

    void OnEndTimer(Timer sender, float currentTime)
    {
        AudioManager.instance.StopAllSounds();
        AudioManager.instance.Play("Customer_Enter");
        if (Score.self.targetProgress >= levelsData.GetLevel(0).firstStarScore)
            WinLevel();
        else if (Score.self.targetProgress < levelsData.GetLevel(0).firstStarScore)
            LoseLevel();
    }


    public void LoseLevel()
    {
        if (!LevelManager.levelManagerInstance.isLoading)
            ui_animator.SetTrigger("Defeat");
    }

    public void WinLevel()
    {
        if (!LevelManager.levelManagerInstance.isLoading)
            ui_animator.SetTrigger("Victory");
    }
}
