using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement section")]
    public float movementSpeed;
    public float minSpeedValue;
    [Header("Hands section")]
    public float grabRange;
    public float throwDistance;
    public float throwFlightTime;
    [Header("Dash section")]
    public float dashDistance;
    public float dashDuration;
    public float dashCooldown;
}
