using UnityEngine;
public interface IThrowable
{
    float Weight { get; set; }
    GameObject Self { get; set; }
    bool IsInsidePlayerRange { get; set; }
    void AttachToPlayer(GameObject playerHand);
    void DetachFromPlayer(float throwDistance, float flightTime);

}
