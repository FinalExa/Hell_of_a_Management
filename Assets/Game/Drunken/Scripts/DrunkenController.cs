using UnityEngine;
using UnityEngine.AI;

public class DrunkenController : MonoBehaviour
{
    [HideInInspector] public DrunkenReferences drunkenReferences;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject player;
    public BoxCollider range;
    public bool playerInRange;

    private void Awake()
    {
        drunkenReferences = this.gameObject.GetComponent<DrunkenReferences>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Start()
    {
        navMeshAgent.speed = drunkenReferences.drunkenData.drunkenSpeed;
        navMeshAgent.acceleration = drunkenReferences.drunkenData.drunkenAcceleration;
        range.size = new Vector3(drunkenReferences.drunkenData.rangeSize, range.size.y, drunkenReferences.drunkenData.rangeSize);
    }
}
