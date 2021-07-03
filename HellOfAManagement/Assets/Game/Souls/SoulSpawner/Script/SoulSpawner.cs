using UnityEngine;
public class SoulSpawner : Spawner
{
    private BoxCollider thisTrigger;
    [SerializeField] private int soulsPerType;

    public override void Awake()
    {
        base.Awake();
        thisTrigger = this.gameObject.GetComponent<BoxCollider>();
        SoulGrabbed.soulIsGrabbed += RemoveSingleSoulFromList;
        SoulIdle.soulIsIdle += AddSingleSoulToList;
        SoulEscapePlayer.soulIsEscapingPlayer += AddSingleSoulToList;
    }

    public override void Start()
    {
        CalculateObjectsToInstantiate();
        base.Start();
        DeactivateObjects();
    }
    private void CalculateObjectsToInstantiate()
    {
        objectsToInstantiate = (int)(((thisTrigger.size.x - 1) * (thisTrigger.size.z - 1)) / 3);
    }

    private void SpawnSouls()
    {
        ActivateObjects();
    }
    public override void ObjectActivatedSetup(int indexInObjectsList)
    {
        SoulController sc = (SoulController)objects[indexInObjectsList];
        RandomizePosition(sc);
        SetupSoul(indexInObjectsList, sc);
    }
    private void RandomizePosition(SoulController sc)
    {
        Vector3 positionToSpawn;
        float xFixedSize = thisTrigger.size.x / 2 - 1;
        float zFixedSize = thisTrigger.size.z / 2 - 1;
        positionToSpawn = new Vector3(Random.Range(-xFixedSize, xFixedSize), 0f, Random.Range(-zFixedSize, zFixedSize));
        sc.gameObject.transform.localPosition = positionToSpawn;
    }
    private void SetupSoul(int indexInObjectsList, SoulController sc)
    {
        int soulIndex;
        if (indexInObjectsList < sc.soulTypes.Length * soulsPerType) soulIndex = indexInObjectsList / soulsPerType;
        else soulIndex = Random.Range(0, sc.soulTypes.Length);
        sc.thisSoulTypeIndex = soulIndex;
        sc.soulReferences.highlightable.thisGraphicsObject = sc.soulTypes[soulIndex].soulMeshContainer;
        sc.soulReferences.highlightable.outline = sc.soulTypes[soulIndex].soulModelOutline;
        sc.soulReferences.highlightable.outline.OutlineColor = sc.soulReferences.highlightable.throwableObjectData.highlightColor;
        sc.soulReferences.soulThrowableObject.thisGraphicsObject = sc.soulTypes[soulIndex].soulMeshContainer;
        sc.soulReferences.soulThrowableObject.SetBaseColor();
        sc.DeactivateAllSoulModels();
        sc.soulTypes[soulIndex].soulMainModelObject.SetActive(true);
    }
    public void AddSingleSoulToList(SoulController soul)
    {
        if (!activeObjects.Contains(soul)) activeObjects.Add(soul);
    }
    public void RemoveSingleSoulFromList(SoulController soul)
    {
        activeObjects.Remove(soul);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SpawnSouls();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) DeactivateObjects();
    }
}
