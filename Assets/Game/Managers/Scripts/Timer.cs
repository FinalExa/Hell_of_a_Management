using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private LevelData thisLevelData;
    [SerializeField] private bool isTutorialLevel;
    public static Timer self;
    [HideInInspector] public float totaltime;
    public float currentTime;
    public bool runTimer => currentTime > 0;
    public bool IsActive { private set; get; } = false;

    public static event Action<Timer, float> OnEndTimer;
    public static event Action<Timer, float> OnTimeUpdate;

    private void Start()
    {
        Init();
        currentTime = totaltime;
    }

    void Init()
    {
        self = this;
        totaltime = thisLevelData.GetLevel(0).levelTimer;
    }

    void Update()
    {
        if (IsActive && !isTutorialLevel) UpdateTime();
    }

    void UpdateTime()
    {
        currentTime = currentTime - Time.deltaTime > 0 ? currentTime - Time.deltaTime : 0;
        OnTimeUpdate?.Invoke(this, currentTime / totaltime);
        if (currentTime == 0)
        {
            TimerFinish();
        }
    }

    public void TimerFinish()
    {
        DeactivateTimer();
        OnEndTimer?.Invoke(this, currentTime / totaltime);
    }

    public static void ActivateTimer() => self.IsActive = true;
    public static void DeactivateTimer() => self.IsActive = false;
}
