using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrunkenController : MonoBehaviour
{
    [HideInInspector]public NavMeshAgent navMeshAgent;
    [HideInInspector]public GameObject player;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }
}
