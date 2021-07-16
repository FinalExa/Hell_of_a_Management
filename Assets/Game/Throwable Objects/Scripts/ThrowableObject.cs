using UnityEngine;

public class ThrowableObject : MonoBehaviour, IThrowable
{
    public float Weight { get; set; }
    public GameObject Self { get; set; }
    public bool IsInsidePlayerRange { get; set; }
    private float throwSpeed;
    private float flightTimer;
    private float throwDistance;
    private float flightTime;
    public ThrowableObjectData throwableObjectData;
    public GameObject thisGraphicsObject;
    [HideInInspector] public bool IsAttachedToHand { get; set; }
    [HideInInspector] public bool isFlying;
    private BoxCollider physicsCollider;
    private GameObject baseContainer;
    [HideInInspector] public Rigidbody selfRB;
    [SerializeField] private string parentObjectTag;

    public virtual void Awake()
    {
        physicsCollider = this.gameObject.GetComponent<BoxCollider>();
        baseContainer = GameObject.FindGameObjectWithTag(parentObjectTag);
        Self = this.gameObject;
        selfRB = Self.GetComponent<Rigidbody>();
    }
    public virtual void Start()
    {
        Weight = throwableObjectData.objectWeight;
        this.gameObject.transform.SetParent(baseContainer.transform);
    }
    void FixedUpdate()
    {
        if (isFlying) FlightTime();
    }
    public virtual void AttachToPlayer(GameObject playerHand)
    {
        StopForce();
        IsAttachedToHand = true;
        ActivateConstraints();
        this.gameObject.transform.position = playerHand.transform.position;
        this.gameObject.transform.SetParent(playerHand.transform);
        this.gameObject.transform.localRotation = Quaternion.identity;
        physicsCollider.enabled = false;
    }
    public virtual void DetachFromPlayer(float throwDistanceObtained, float flightTimeObtained)
    {
        throwDistance = throwDistanceObtained;
        flightTime = flightTimeObtained;
        DeactivateConstraintsExceptGravity();
        this.gameObject.transform.SetParent(baseContainer.transform);
        IsAttachedToHand = false;
        physicsCollider.enabled = true;
        LaunchSelf();
    }
    private void LaunchSelf()
    {
        throwSpeed = throwDistance / flightTime;
        flightTimer = flightTime;
        selfRB.velocity = new Vector3(transform.forward.x, transform.forward.y, transform.forward.z) * throwSpeed;
        isFlying = true;
    }
    private void FlightTime()
    {
        if (flightTimer > 0) flightTimer -= Time.deltaTime;
        else
        {
            DeactivateConstraintsTotally();
            isFlying = false;
        }
    }
    private void StopForce()
    {
        isFlying = false;
        selfRB.velocity = Vector3.zero;
    }
    private void ActivateConstraints()
    {
        selfRB.constraints = RigidbodyConstraints.FreezeAll;
    }
    private void DeactivateConstraintsExceptGravity()
    {
        selfRB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    private void DeactivateConstraintsTotally()
    {
        selfRB.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("Ground"))
        {
            StopForce();
            DeactivateConstraintsTotally();
        }
    }
}
