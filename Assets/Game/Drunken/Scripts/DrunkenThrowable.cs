using UnityEngine;

public class DrunkenThrowable : ThrowableObject
{
    [HideInInspector] public bool isNotGrounded;
    public override void Start()
    {
        base.Start();
        isNotGrounded = false;
    }
    public override void AttachToPlayer(GameObject playerHand)
    {
        isNotGrounded = true;
        base.AttachToPlayer(playerHand);
    }
    public override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if (collision.gameObject.CompareTag("Ground")) isNotGrounded = false;
    }
}
