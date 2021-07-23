using UnityEngine;

[CreateAssetMenu(fileName = "CustomerData", menuName = "ScriptableObjects/CustomerData", order = 7)]
public class CustomerData : ScriptableObject
{
    public float customerMovementSpeed;
    public float customerAcceleration;
    [System.Serializable]
    public struct OrderSizesProbabilitiesAndScore
    {
        [Range(0f, 100f)] public float probabilityOfThisOrderSize;
        public float scoreGivenByThisOrderSize;
    }
    public OrderSizesProbabilitiesAndScore[] orderSizesProbabilitiesAndScores;

    public int maxActiveOrdersAtATime;
    [HideInInspector] public int activeOrders;
    public float timeBetweenSpawns;
    [Range(0f, 100f)] public int mudTerrainSpawnChance;
    [Range(0f, 100f)] public int icyTerrainSpawnChance;
}
