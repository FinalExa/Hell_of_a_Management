﻿using UnityEngine;
using UnityEngine.AI;

public class DrunkenController : MonoBehaviour
{
    [HideInInspector] public DrunkenReferences drunkenReferences;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public Rigidbody thisRb;
    [HideInInspector] public GameObject player;
    [HideInInspector] public bool playerInRange;
    public float grabHeightOffset;

    private void Awake()
    {
        drunkenReferences = this.gameObject.GetComponent<DrunkenReferences>();
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        thisRb = this.gameObject.GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Start()
    {
        navMeshAgent.speed = drunkenReferences.drunkenData.drunkenSpeed;
        navMeshAgent.acceleration = drunkenReferences.drunkenData.drunkenAcceleration;
    }
}
