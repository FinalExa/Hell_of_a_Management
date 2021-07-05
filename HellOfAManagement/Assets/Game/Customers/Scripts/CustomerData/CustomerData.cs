using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "ScriptableObjects/CustomerData", order = 7)]
public class CustomerData : ScriptableObject
{
    public float customerMovementSpeed;
    public float customerAcceleration;
    [System.Serializable]
    public struct OrderSizesProbabilitiesAndScore
    {
        [Range(0f, 100f)]
        public float probabilityOfThisOrderSize;
        public float scoreGivenByThisOrderSize;
    }
    public OrderSizesProbabilitiesAndScore[] orderSizesProbabilitiesAndScores;
}
