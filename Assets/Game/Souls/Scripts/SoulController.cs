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
        soulReferences = this.gameObject.GetComponent<SoulReferences>();
        exit = GameObject.FindGameObjectWithTag("Exit");
        storageRoom = GameObject.FindGameObjectWithTag("StorageRoom").GetComponent<BoxCollider>();
    }
    private void Start()
    {
        thisNavMeshAgent.speed = soulReferences.soulData.soulMovementSpeed;
        thisNavMeshAgent.acceleration = soulReferences.soulData.soulAcceleration;
    }
    private void OnDisable()
    {
        isInsideExitDoorCollider = false;
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
        ICanUseIngredients canUseIngredients = other.GetComponent<ICanUseIngredients>();
        canUseIngredients.RecipeFill(soulTypes[thisSoulTypeIndex].soulColor, this);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit")) isInsideExitDoorCollider = true;
        if (other.CompareTag("Machine")) AttemptToEnterMachine(other);
    }
    public bool SoulIsInsideStorage()
    {
        bool isInside = false;
        bool xTrue = false;
        bool zTrue = false;
        Vector3 thisPos = this.gameObject.transform.position;
        Vector3 storageRoomPos = storageRoom.gameObject.transform.position;
        if ((thisPos.x <= storageRoomPos.x + storageRoom.size.x) && (thisPos.x >= storageRoomPos.x - storageRoom.size.x)) xTrue = true;
        if ((thisPos.z <= storageRoomPos.z + storageRoom.size.z) && (thisPos.z >= storageRoomPos.z - storageRoom.size.z)) zTrue = true;
        if (xTrue && zTrue) isInside = true;
        return isInside;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Exit") && !other.CompareTag("Player")) isInsideExitDoorCollider = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Soul")) collidedWithOther = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") && !collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Soul")) collidedWithOther = false;
    }
}
