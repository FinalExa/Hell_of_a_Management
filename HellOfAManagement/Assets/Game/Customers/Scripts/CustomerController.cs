using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour, ICanBeInteracted
{
    public static Action<Table, int, CustomerController> customerLeft;
    [SerializeField] private GameObject[] customerModels;
    [SerializeField] private float maxInteractionTimer;
    private float interactionTimer;
    [HideInInspector] public GameObject exitDoor;
    public GameObject seatToTake;
    [HideInInspector] public GameObject targetedLocation;
    [HideInInspector] public Table thisTable;
    [HideInInspector] public int thisTableId;
    [HideInInspector] public bool interactionReceived;
    [HideInInspector] public bool waitingForOrder;
    [HideInInspector] public bool leave;
    public NavMeshAgent thisNavMeshAgent;
    public Order.OrderType[] possibleTypes;
    public SoulType.SoulColor[] possibleIngredients;
    [HideInInspector] public Order.OrderType chosenType;
    [HideInInspector] public List<SoulType.SoulColor> chosenIngredients;
    private Vector3 startingPos;

    [HideInInspector] public CustomerReferences customerReferences;

    public GameObject Self { get; set; }
    private GameObject selectedModel;

    private void Awake()
    {
        customerReferences = this.gameObject.GetComponent<CustomerReferences>();
        Self = this.gameObject;
        exitDoor = GameObject.FindGameObjectWithTag("Exit");
        startingPos = this.gameObject.transform.position;
    }
    private void OnEnable()
    {
        RandomizeModel();
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
    public void RandomizeModel()
    {
        int randIndex = UnityEngine.Random.Range(0, customerModels.Length);
        selectedModel = customerModels[randIndex];
        selectedModel.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            customerLeft(thisTable, thisTableId, this);
            this.gameObject.SetActive(false);
        }
    }
}
