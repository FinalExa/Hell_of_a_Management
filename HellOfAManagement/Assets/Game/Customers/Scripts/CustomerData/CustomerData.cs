using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "ScriptableObjects/CustomerData", order = 7)]
public class CustomerData : ScriptableObject
{
    public float customerMovementSpeed;
    public float customerAcceleration;
}
