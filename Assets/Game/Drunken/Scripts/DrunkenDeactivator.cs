using System;
using UnityEngine;

public class DrunkenDeactivator : MonoBehaviour
{
    public static Action<float> giveScore;
    private bool inThePub;
    private DrunkenReferences drunkenReferences;
    [SerializeField] private float deleteTimer;
    private float timer;
    private bool deleteTimerOn;
    private void Awake()
    {
        drunkenReferences = this.gameObject.GetComponent<DrunkenReferences>();
    }
    private void OnEnable()
    {
        inThePub = false;
        deleteTimerOn = false;
        timer = deleteTimer;
    }

    private void Update()
    {
        if (deleteTimerOn) DeleteTimer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Remover") && inThePub && !drunkenReferences.throwableObject.IsAttachedToHand)
        {
            deleteTimerOn = true;
        }
        if (other.gameObject.CompareTag("InThePub")) inThePub = true;
    }

    private void DeleteTimer()
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            giveScore(drunkenReferences.drunkenData.scoreGiven);
            this.gameObject.SetActive(false);
        }
    }
}
