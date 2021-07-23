using System.Collections.Generic;
using UnityEngine;
public class CustomerSpawner : Spawner
{
    [SerializeField] private CustomerData customerData;
    private float timeBetweenSpawns;
    private float spawnerTimer;
    private bool firstSpawn;
    private bool spawnerIsFilled;
    [HideInInspector] public List<SeatInfo> freeSeats;
    [HideInInspector] public List<MonoBehaviour> inactiveCustomers;
    private Table[] tablesList;
    public override void Awake()
    {
        CustomerController.customerLeft += CustomerLeft;
        base.Awake();
    }
    public override void Start()
    {
        customerData.activeOrders = 0;
        timeBetweenSpawns = customerData.timeBetweenSpawns;
        spawnerTimer = timeBetweenSpawns;
        firstSpawn = true;
        CalculateObjectsToInstantiate();
        base.Start();
        AddInactiveCustomers();
        SetupFreeSeats();
    }
    private void Update()
    {
        if (!spawnerIsFilled) SpawnerTimer();
    }
    private void CalculateObjectsToInstantiate()
    {
        tablesList = FindObjectsOfType<Table>();
        objectsToInstantiate = tablesList.Length * 4;
    }
    private void AddInactiveCustomers()
    {
        for (int i = 0; i < objectsToInstantiate; i++) inactiveCustomers.Add(objects[i]);
    }
    private void SetupFreeSeats()
    {
        foreach (Table table in tablesList)
        {
            for (int i = 0; i < table.seatInfo.Length; i++)
            {
                freeSeats.Add(table.seatInfo[i]);
            }
        }
    }

    private void SpawnerTimer()
    {
        if (firstSpawn)
        {
            SpawnCustomer();
            AudioManager.instance.Play("Customer_Enter");
            firstSpawn = false;
        }
        if (spawnerTimer > 0) spawnerTimer -= Time.deltaTime;
        else
        {
            SpawnCustomer();
            AudioManager.instance.Play("Customer_Enter");
        }
    }
    private void SpawnCustomer()
    {
        SearchForFreeSeat();
        spawnerTimer = timeBetweenSpawns;
        if (activeObjects.Count == objects.Count) spawnerIsFilled = true;
    }

    private void SearchForFreeSeat()
    {
        int currentIndex = activeObjects.Count;
        activeObjects.Add(inactiveCustomers[0]);
        inactiveCustomers.RemoveAt(0);
        CustomerController cc = (CustomerController)activeObjects[currentIndex];
        int randomIndex = Random.Range(0, freeSeats.Count);
        StartupCustomer(cc, randomIndex);
    }

    private void StartupCustomer(CustomerController cc, int seatIndex)
    {
        cc.seatToTake = freeSeats[seatIndex].seatTarget;
        cc.thisTable = freeSeats[seatIndex].thisTable;
        freeSeats[seatIndex].customer = cc;
        cc.thisTableSeatId = freeSeats[seatIndex].thisId;
        cc.targetedLocation = cc.seatToTake;
        cc.gameObject.SetActive(true);
        freeSeats.RemoveAt(seatIndex);
    }

    private void CustomerLeft(Table table, int id, CustomerController customer)
    {
        activeObjects.Remove(customer);
        inactiveCustomers.Add(customer);
        freeSeats.Add(table.seatInfo[id]);
        if (spawnerIsFilled) spawnerIsFilled = false;
    }
}
