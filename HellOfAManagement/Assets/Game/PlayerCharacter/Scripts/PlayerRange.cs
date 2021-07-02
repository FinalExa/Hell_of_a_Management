using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    private PlayerController playerController;
    private SphereCollider playerRange;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        playerRange = this.gameObject.GetComponent<SphereCollider>();
    }
    private void Start()
    {
        playerRange.radius = playerController.playerReferences.playerData.grabRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        GetIThrowable(other);
        GetICanBeInteracted(other);
    }

    private void GetIThrowable(Collider other)
    {
        IThrowable otherObject = other.gameObject.GetComponent<IThrowable>();
        if (otherObject != null)
        {
            Collider otherCol = otherObject.Self.GetComponent<Collider>();
            if (!playerController.throwablesInPlayerRange.Contains(otherCol))
            {
                otherObject.IsInsidePlayerRange = true;
                playerController.throwablesInPlayerRange.Add(otherCol);
            }
        }
    }

    private void GetICanBeInteracted(Collider other)
    {
        ICanBeInteracted otherObject = other.gameObject.GetComponent<ICanBeInteracted>();
        if (otherObject != null)
        {
            Collider otherCol = otherObject.Self.GetComponent<Collider>();
            if (!playerController.interactablesInPlayerRange.Contains(otherCol))
            {
                playerController.interactablesInPlayerRange.Add(otherCol);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        ThrowableIsOutOfRange(other);
        InteractableIsOutOfRange(other);
    }

    private void ThrowableIsOutOfRange(Collider other)
    {
        IThrowable otherObject = other.gameObject.GetComponent<IThrowable>();
        if (otherObject != null && Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.z)) > playerController.playerReferences.playerData.grabRange)
        {
            Collider otherCol = otherObject.Self.GetComponent<Collider>();
            if (playerController.throwablesInPlayerRange.Contains(otherCol))
            {
                otherObject.IsInsidePlayerRange = false;
                playerController.throwablesInPlayerRange.Remove(otherCol);
            }
        }
    }
    private void InteractableIsOutOfRange(Collider other)
    {
        ICanBeInteracted otherObject = other.gameObject.GetComponent<ICanBeInteracted>();
        if (otherObject != null && Vector2.Distance(new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.z), new Vector2(other.gameObject.transform.position.x, other.gameObject.transform.position.z)) > playerController.playerReferences.playerData.grabRange)
        {
            Collider otherCol = otherObject.Self.GetComponent<Collider>();
            if (playerController.interactablesInPlayerRange.Contains(otherCol))
            {
                playerController.interactablesInPlayerRange.Remove(otherCol);
            }
        }
    }
}
