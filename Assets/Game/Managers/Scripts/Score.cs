﻿using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score self;
    public float targetProgress { set; get; } = 0;

    #region Events
    public static event Action<Score, float> OnProgressChanged;
    #endregion
    private void Awake()
    {
        targetProgress = 0;
        CustomerWaitingForOrder.addScore += AddScore;
        Tutorial.tutorialAddScore += AddScore;
        DrunkenDeactivator.drunkenDefeated += AddScore;
    }
    void Start()
    {
        Init();
    }

    void Init()
    {
        self = this;
    }

    public void AddScore(float score)
    {
        targetProgress = Mathf.Clamp(targetProgress + score, 0, 66666666);
        OnProgressChanged?.Invoke(this, targetProgress);
    }
}
