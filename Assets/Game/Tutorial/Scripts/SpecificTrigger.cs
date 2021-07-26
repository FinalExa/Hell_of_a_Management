using System;
using UnityEngine;

public class SpecificTrigger : MonoBehaviour
{
    public static Action tutorialAdvance;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            tutorialAdvance();
            this.gameObject.SetActive(false);
        }
    }
}
