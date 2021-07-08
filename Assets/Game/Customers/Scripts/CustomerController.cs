using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour, ICanBeInteracted
{
    public bool IsInsidePlayerRange { get; set; }
    public static Action<Table, int, CustomerController> customerLeft;
    [SerializeField] private OrdersData ordersData;
    [SerializeField] private float maxInteractionTimer;
    public NavMeshAgent thisNavMeshAgent;
    private float interactionTimer;
    private Vector3 startingPos;
    private CustomerVignetteToDoList uiToDoList;
    [HideInInspector] public GameObject seatToTake;
    [HideInInspector] public GameObject exitDoor;
    [HideInInspector] public GameObject targetedLocation;
    [HideInInspector] public Table thisTable;
    [HideInInspector] public int thisTableSeatId;
    [HideInInspector] public bool interactionReceived;
    [HideInInspector] public bool waitingForOrder;
    [HideInInspector] public bool leave;
    [HideInInspector] public OrdersData.OrderTypes[] possibleTypes;
    [HideInInspector] public OrdersData.OrderIngredients[] possibleIngredients;
    [HideInInspector] public OrdersData.OrderTypes chosenType;
    [HideInInspector] public List<OrdersData.OrderIngredients> chosenIngredients;
    [HideInInspector] public CustomerReferences customerReferences;
    public GameObject Self { get; set; }

    private void Awake()
    {
        customerReferences = this.gameObject.GetComponent<CustomerReferences>();
        Self = this.gameObject;
        exitDoor = GameObject.FindGameObjectWithTag("Exit");
    }

    private void Start()
    {
        startingPos = this.gameObject.transform.position;
        thisNavMeshAgent.speed = customerReferences.customerData.customerMovementSpeed;
        thisNavMeshAgent.acceleration = customerReferences.customerData.customerAcceleration;
        InitializeOrderInfos();
    }
    private void OnEnable()
    {
        targetedLocation = seatToTake;
    }
    private void OnDisable()
    {
        leave = false;
        this.gameObject.transform.position = startingPos;
    }
    private void Update()
    {
        if (interactionReceived) InteractionTimer();
    }
    private void InitializeOrderInfos()
    {
        possibleTypes = ordersData.orderTypes;
        possibleIngredients = ordersData.orderIngredients;
    }
    public void Interaction()
    {
        interactionReceived = true;
        interactionTimer = maxInteractionTimer;
    }
    private void InteractionTimer()
    {
        if (interactionTimer > 0) interactionTimer -= Time.deltaTime;
        else
        {
            interactionReceived = false;
            interactionTimer = maxInteractionTimer;
        }
    }
    public void SendInfoToToDoList()
    {
        if (uiToDoList == null) uiToDoList = FindObjectOfType<CustomerVignetteToDoList>();
        uiToDoList.AddOrder(chosenType, chosenIngredients);
    }
    public void RemoveInfoFromToDoList()
    {
        uiToDoList.RemoveOrder(chosenType, chosenIngredients);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            customerLeft(thisTable, thisTableSeatId, this);
            this.gameObject.SetActive(false);
        }
    }
}
