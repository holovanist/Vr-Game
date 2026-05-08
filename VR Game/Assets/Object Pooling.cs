using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    public Transform parentTransform;

    int lastUsedIndex = -1;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool, parentTransform != null ? parentTransform : this.transform);
            tmp.GetComponent<MeshRenderer>().enabled = false;
            tmp.GetComponent<SphereCollider>().enabled = false;
            //tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }
    public GameObject GetPooledObject()
    {
        int startIndex = (lastUsedIndex + 1) % amountToPool; // Move to the next object in the pool
        for (int i = 0; i < amountToPool; i++)
        {
            int currentIndex = (startIndex + i) % amountToPool;
            if (pooledObjects[currentIndex].activeInHierarchy)
            {
                lastUsedIndex = currentIndex; // Update last used index
                return pooledObjects[currentIndex];
            }
        }
        return null;
    }
}
