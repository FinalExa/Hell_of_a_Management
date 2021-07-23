using UnityEngine;

public class PlayerInRange : MonoBehaviour
{
    private SoulController soulController;
    public BoxCollider thisTrigger;

    private void Awake()
    {
        soulController = this.gameObject.transform.parent.GetComponent<SoulController>();
    }
    private void Start()
    {
        thisTrigger.size = new Vector3(soulController.soulReferences.soulData.soulDetectionRange, thisTrigger.size.y, soulController.soulReferences.soulData.soulDetectionRange);
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
