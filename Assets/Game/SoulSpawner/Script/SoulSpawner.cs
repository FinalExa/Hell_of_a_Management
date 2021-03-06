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
    }
    private void CalculateObjectsToInstantiate()
    {
        objectsToInstantiate = (int)(((thisTrigger.size.x - 1) * (thisTrigger.size.z - 1)) / 3);
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
        positionToSpawn = new Vector3(Random.Range(-xFixedSize, xFixedSize) + thisTrigger.center.x, 0f, Random.Range(-zFixedSize, zFixedSize) + thisTrigger.center.z);
        sc.gameObject.transform.localPosition = positionToSpawn;
    }
    private void SetupSoul(int indexInObjectsList, SoulController sc)
    {
        int soulIndex;
        if (indexInObjectsList < sc.soulTypes.Length * soulsPerType) soulIndex = indexInObjectsList / soulsPerType;
        else soulIndex = Random.Range(0, sc.soulTypes.Length);
        sc.gameObject.SetActive(true);
        sc.soulReferences.soulStateMachine.SetState(new SoulIdle(sc.soulReferences.soulStateMachine));
        sc.thisNavMeshAgent.enabled = true;
        sc.soulReferences.soulAnimations.animator = sc.soulTypes[soulIndex].soulAnimator;
        sc.thisSoulTypeIndex = soulIndex;
        sc.soulReferences.highlightable.thisGraphicsObject = sc.soulTypes[soulIndex].soulMeshContainer;
        sc.soulReferences.highlightable.outline = sc.soulTypes[soulIndex].soulModelOutline;
        sc.soulReferences.highlightable.outline.OutlineColor = sc.soulReferences.highlightable.outlineData.highlightColor;
        sc.soulReferences.highlightable.outline.OutlineWidth = sc.soulReferences.highlightable.outlineData.outlineWidth;
        sc.soulReferences.highlightable.DeactivateGraphic();
        sc.soulReferences.soulThrowableObject.thisGraphicsObject = sc.soulTypes[soulIndex].soulMeshContainer;
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
        if (other.CompareTag("Player")) ActivateObjects();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) DeactivateObjects();
    }
}
