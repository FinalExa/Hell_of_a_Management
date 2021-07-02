using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    private SoulController soulController;
    private SphereCollider thisTrigger;

    private void Awake()
    {
        soulController = this.gameObject.transform.parent.GetComponent<SoulController>();
        thisTrigger = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        thisTrigger.radius = soulController.soulReferences.soulData.soulDetectionRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) soulController.playerIsInRange = other.gameObject;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) soulController.playerIsInRange = null;
    }
}
