using System;
using UnityEngine;

public class SpecificTrigger : MonoBehaviour
{
    public static Action continueTutorial;
    [SerializeField] private int thisActivationIndex;
    private Tutorial tutorial;
    private void Awake()
    {
        tutorial = FindObjectOfType<Tutorial>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && tutorial.tutorialIndex == thisActivationIndex && !this.gameObject.CompareTag("DeactivateInTrigger"))
        {
            continueTutorial();
            this.gameObject.SetActive(false);
        }
        if (this.gameObject.CompareTag("DeactivateInTrigger") && tutorial.tutorialIndex == thisActivationIndex)
        {
            tutorial.ArrowDeactivate();
            this.gameObject.SetActive(false);
        }
    }
}
