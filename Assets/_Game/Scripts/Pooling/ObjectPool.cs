using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool PoolInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountPool;

    private void Awake()
    {
        PoolInstance = this;
    }


    private void Start()
    {
        pooledObjects=new List<GameObject>();
        GameObject newObject;
        for (int i=0;i<amountPool;i++)
        {
            newObject = Instantiate(objectToPool);
            newObject.SetActive(false);
            pooledObjects.Add(newObject);
        }

    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
