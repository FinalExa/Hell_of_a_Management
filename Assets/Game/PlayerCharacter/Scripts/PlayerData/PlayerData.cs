using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    [Header("Movement section")]
    [HideInInspector]public float currentMovementSpeed;
    public float defaultMovementSpeed;
    public float slowMovementSpeed;
    public float fastMovementSpeed;
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
