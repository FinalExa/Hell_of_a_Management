using System;
using UnityEngine;
public class OrderThrowable : ThrowableObject
{
    public static Action<OrdersData.OrderTypes, Vector3> orderFallenDown;
    private Order order;
    private bool isLaunched;
    private bool isGrounded;

    public override void Awake()
    {
        base.Awake();
        order = this.gameObject.GetComponent<Order>();
    }
    public override void Start()
    {
        base.Start();
        isLaunched = false;
    }
    public override void DetachFromPlayer(float throwDistanceObtained, float flightTimeObtained)
    {
        base.DetachFromPlayer(throwDistanceObtained, flightTimeObtained);
        isLaunched = true;
    }
    private void Update()
    {
        if (isLaunched && isGrounded) LandOnGround();
    }

    private void LandOnGround()
    {
        isLaunched = false;
        orderFallenDown(order.thisOrderType, this.gameObject.transform.position);
    }

    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }
}
