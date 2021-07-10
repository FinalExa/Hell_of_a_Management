using UnityEngine;
public class OrderThrowable : ThrowableObject
{
    private bool isLaunched;
    private bool isGrounded;

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
        print("Generate Terrain Wrong Throw");
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
