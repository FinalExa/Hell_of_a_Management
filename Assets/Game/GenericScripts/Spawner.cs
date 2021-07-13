using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    protected int objectsToInstantiate;
    protected int objectsToSpawn;
    [SerializeField] protected List<MonoBehaviour> objects;
    [SerializeField] protected List<MonoBehaviour> activeObjects;
    [SerializeField] protected MonoBehaviour objectReference;
    [SerializeField] private string parentObjectTag;
    private GameObject parentObject;
    public SpawnerData spawnerData;

    public virtual void Awake()
    {
        parentObject = GameObject.FindGameObjectWithTag(parentObjectTag);
    }

    public virtual void Start()
    {
        CreateObjects();
        if (spawnerData != null) SetObjectsToSpawnNumber();
    }

    public virtual void CreateObjects()
    {
        for (int i = 0; i < objectsToInstantiate; i++)
        {
            objects.Add(Instantiate(objectReference, parentObject.transform));
        }
    }
    private void SetObjectsToSpawnNumber()
    {
        objectsToSpawn = spawnerData.objectsToSpawn;
        if (objectsToSpawn < 0 || objectsToSpawn > objectsToInstantiate) objectsToSpawn = (int)Mathf.Lerp(0, objectsToInstantiate, 1);
    }
    public virtual void ActivateObjects()
    {
        int countSpawnedObjects = objectsToSpawn - (objectsToSpawn - activeObjects.Count);
        for (int i = 0; i < objectsToInstantiate; i++)
        {
            if (!objects[i].gameObject.activeSelf)
            {
                ObjectActivatedSetup(i);
                if (!activeObjects.Contains(objects[i]))
                {
                    activeObjects.Add(objects[i]);
                }
                countSpawnedObjects++;
            }
            if (countSpawnedObjects == objectsToSpawn) break;
        }
    }
    public virtual void DeactivateObjects()
    {
        for (int i = 0; i < activeObjects.Count; i++)
        {
            activeObjects[i].gameObject.SetActive(false);
            activeObjects.RemoveAt(i);
            i--;
        }
    }

    public virtual void ObjectActivatedSetup(int indexInObjectsList)
    {
        return;
    }
}
