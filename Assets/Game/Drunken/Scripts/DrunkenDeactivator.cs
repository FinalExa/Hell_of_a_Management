using System;
using UnityEngine;

public class DrunkenDeactivator : MonoBehaviour
{
    public static Action<DrunkenController> drunkenDefeat;
    public static Action<float> drunkenDefeated;
    private bool inThePub;
    private DrunkenController drunkenController;
    [SerializeField] private float deleteTimer;
    private float timer;
    private bool deleteTimerOn;
    private void Awake()
    {
        drunkenController = this.gameObject.GetComponent<DrunkenController>();
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
        if (other.gameObject.CompareTag("Remover") && inThePub && !drunkenController.drunkenReferences.throwableObject.IsAttachedToHand)
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
            drunkenDefeat(drunkenController);
            drunkenDefeated(drunkenController.drunkenReferences.drunkenData.scoreGiven);
            this.gameObject.SetActive(false);
        }
    }
}
