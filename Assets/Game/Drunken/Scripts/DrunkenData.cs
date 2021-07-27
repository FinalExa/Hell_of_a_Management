using UnityEngine;

[CreateAssetMenu(fileName = "DrunkenData", menuName = "ScriptableObjects/DrunkenData", order = 9)]
public class DrunkenData : ScriptableObject
{
    public float drunkenSpeed;
    public float drunkenAcceleration;
    public float rangeSize;
    public float scoreGiven;
    public float timerDeny;
}
