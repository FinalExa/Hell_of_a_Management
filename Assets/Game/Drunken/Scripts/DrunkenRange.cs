using UnityEngine;

public class DrunkenRange : MonoBehaviour
{
    private DrunkenController drunkenController;

    private void Awake()
    {
        drunkenController = GetComponentInParent<DrunkenController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            drunkenController.playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            drunkenController.playerInRange = false;
        }
    }
}
