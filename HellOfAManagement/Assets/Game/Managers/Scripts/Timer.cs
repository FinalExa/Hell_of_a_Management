using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public static Timer self;
    public float totaltime = 60;
    public float currentTime;
    public bool runTimer => currentTime > 0;
    public bool IsActive { private set; get; } = false;

    public static event Action<Timer, float>OnEndTimer;
    public static event Action<Timer, float> OnTimeUpdate;

    private void Start()
    {
        Init();
        currentTime = totaltime;
    }

    void Init()
    {
        self = this;
    }

    void Update()
    {
        if (IsActive) UpdateTime();
    }

    //void TimerGoingDown()
    //{
    //    if (runTimer)
     //   {
    //        currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0, currentTime);
    //        if (currentTime == 0)
    //            OnEndTimer?.Invoke(this, currentTime);
    //    }
    //}

    void UpdateTime()
    {
        currentTime = currentTime - Time.deltaTime > 0 ? currentTime - Time.deltaTime : 0;
        OnTimeUpdate?.Invoke(this, currentTime / totaltime);
        if(currentTime == 0)
        {
            DeactivateTimer();
            OnEndTimer?.Invoke(this, currentTime / totaltime);
        }
    }

    public static void ActivateTimer() => self.IsActive = true;
    public static void DeactivateTimer() => self.IsActive = false;
}
