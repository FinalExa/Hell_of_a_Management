using System.Collections.Generic;
using UnityEngine;

public class DrunkenSpawner : Spawner
{
    [SerializeField] private DrunkenSpawnerData drunkenSpawnerData;
    [HideInInspector] public List<MonoBehaviour> inactiveDrunkens;
    private float timeBetweenSpawns;
    private float spawnerTimer;
    private bool spawnerIsFilled;
    public override void Awake()
    {
        DrunkenDeactivator.drunkenDefeat += DrunkenLeft;
        base.Awake();
    }
    public override void Start()
    {
        timeBetweenSpawns = drunkenSpawnerData.timeBetweenSpawns;
        spawnerTimer = timeBetweenSpawns;
        CalculateObjectsToInstantiate();
        base.Start();
        AddInactiveDrunkens();
    }
    private void Update()
    {
        if (!spawnerIsFilled) SpawnerTimer();
    }
    private void CalculateObjectsToInstantiate()
    {
        objectsToInstantiate = drunkenSpawnerData.maxDrunkens;
    }
    private void AddInactiveDrunkens()
    {
        for (int i = 0; i < objectsToInstantiate; i++) inactiveDrunkens.Add(objects[i]);
    }

    private void SpawnerTimer()
    {
        if (spawnerTimer > 0) spawnerTimer -= Time.deltaTime;
        else
        {
            if (Random.Range(1, 100) <= drunkenSpawnerData.drunkenSpawnChance) SpawnDrunken();
            spawnerTimer = timeBetweenSpawns;
        }
    }
    private void SpawnDrunken()
    {
        AudioManager.instance.Play("DrunkenDemon_Enter");
        DrunkenController dc = (DrunkenController)inactiveDrunkens[0];
        StartupDrunken(dc);
        activeObjects.Add(dc);
        dc.drunkenReferences.highlightable.outline.enabled = false;
        inactiveDrunkens.RemoveAt(0);
        if (activeObjects.Count == objects.Count) spawnerIsFilled = true;
    }

    private void StartupDrunken(DrunkenController dc)
    {
        dc.gameObject.SetActive(true);
    }

    private void DrunkenLeft(DrunkenController drunken)
    {
        activeObjects.Remove(drunken);
        inactiveDrunkens.Add(drunken);
        if (spawnerIsFilled) spawnerIsFilled = false;
    }
}
