using UnityEngine;
using UnityEngine.AI;
public class SoulController : MonoBehaviour
{
    [HideInInspector] public bool collidedWithOther;
    [HideInInspector] public bool isInsideExitDoorCollider;
    public NavMeshAgent thisNavMeshAgent;
    public Rigidbody thisRigidbody;
    [HideInInspector] public GameObject playerIsInRange;
    [HideInInspector] public SoulReferences soulReferences;
    [HideInInspector] public GameObject exit;
    [HideInInspector] public BoxCollider storageRoom;
    private BoxCollider thisCollider;
    [System.Serializable]
    public struct SoulType
    {
        public OrdersData.OrderIngredients soulColor;
        public GameObject soulMainModelObject;
        public GameObject soulMeshContainer;
        public Animator soulAnimator;
        public Outline soulModelOutline;
    }
    public SoulType[] soulTypes;
    [HideInInspector] public int thisSoulTypeIndex;
    private void Awake()
    {
        thisCollider = this.gameObject.GetComponent<BoxCollider>();
        soulReferences = this.gameObject.GetComponent<SoulReferences>();
        exit = GameObject.FindGameObjectWithTag("Exit");
        storageRoom = GameObject.FindGameObjectWithTag("StorageRoom").GetComponent<BoxCollider>();
    }
    private void Start()
    {
        thisNavMeshAgent.speed = soulReferences.soulData.soulMovementSpeed;
        thisNavMeshAgent.acceleration = soulReferences.soulData.soulAcceleration;
    }
    public void DeactivateAllSoulModels()
    {
        for (int i = 0; i < soulTypes.Length; i++)
        {
            soulTypes[i].soulMainModelObject.SetActive(false);
        }
    }
    private void AttemptToEnterMachine(Collider other)
    {
        ICanUseIngredients canUseIngredients = other.gameObject.transform.parent.gameObject.GetComponent<ICanUseIngredients>();
        canUseIngredients.RecipeFill(soulTypes[thisSoulTypeIndex].soulColor, this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit")) isInsideExitDoorCollider = true;
        if (other.CompareTag("Machine")) AttemptToEnterMachine(other);
        if (other.CompareTag("PlayerRange")) other.GetComponent<PlayerRange>().AddToPlayerRange(soulReferences.soulThrowableObject.GetComponent<IThrowable>(), thisCollider);
    }
    public bool SoulIsInsideStorage()
    {
        bool isInside = false;
        bool xTrue = false;
        bool zTrue = false;
        Vector3 thisPos = this.gameObject.transform.position;
        Vector3 storageRoomPos = storageRoom.gameObject.transform.position + storageRoom.center;
        if ((thisPos.x <= storageRoomPos.x + storageRoom.size.x / 2) && (thisPos.x >= storageRoomPos.x - storageRoom.size.x / 2)) xTrue = true;
        if ((thisPos.z <= storageRoomPos.z + storageRoom.size.z / 2) && (thisPos.z >= storageRoomPos.z - storageRoom.size.z / 2)) zTrue = true;
        if (xTrue && zTrue) isInside = true;
        return isInside;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Soul")) collidedWithOther = true;
        if (collision.gameObject.CompareTag("Void")) Physics.IgnoreCollision(thisCollider, collision.collider);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Soul")) collidedWithOther = false;
    }
}
