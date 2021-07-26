using UnityEngine;

[CreateAssetMenu(fileName = "DrunkenSpawnerData", menuName = "ScriptableObjects/DrunkenSpawnerData", order = 10)]

public class DrunkenSpawnerData : ScriptableObject
{
    public int maxDrunkens;
    public float timeBetweenSpawns;
    [Range(0, 100)] public int drunkenSpawnChance;
}
