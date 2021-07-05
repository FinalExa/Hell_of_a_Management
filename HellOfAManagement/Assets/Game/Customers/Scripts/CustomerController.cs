using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour, ICanBeInteracted
{
    public bool IsInsidePlayerRange { get; set; }
    public static Action<Table, int, CustomerController> customerLeft;
    [System.Serializable]
    public struct CustomerGraphics
    {
        public GameObject customerModel;
        public Outline customerOutline;
    }
    [SerializeField] private CustomerData customerData;
    [SerializeField] private CustomerGraphics[] customerGraphics;
    [SerializeField] private float maxInteractionTimer;
    private float interactionTimer;
    [HideInInspector] public GameObject seatToTake;
    [HideInInspector] public GameObject exitDoor;
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
    }

    private void Start()
    {
        startingPos = this.gameObject.transform.position;
        thisNavMeshAgent.speed = customerData.customerMovementSpeed;
        thisNavMeshAgent.acceleration = customerData.customerAcceleration;
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
        int randIndex = UnityEngine.Random.Range(0, customerGraphics.Length);
        selectedModel = customerGraphics[randIndex].customerModel;
        selectedModel.SetActive(true);
        customerReferences.highlightable.outline = customerGraphics[randIndex].customerOutline;
        customerReferences.highlightable.outline.OutlineColor = customerReferences.highlightable.outlineData.highlightColor;
        customerReferences.highlightable.outline.OutlineWidth = customerReferences.highlightable.outlineData.outlineWidth;
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
