using UnityEngine;
using UnityEngine.AI;

public class DrunkenController : MonoBehaviour
{
    [HideInInspector] public DrunkenReferences drunkenReferences;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public GameObject player;

    private void Awake()
    {
        drunkenReferences = this.gameObject.GetComponent<DrunkenReferences>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }
}
