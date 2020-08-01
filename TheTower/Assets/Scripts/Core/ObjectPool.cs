using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject objectToPool;
    [SerializeField] private bool willGrow;
    [SerializeField] private int initialSize = 3;
    private List<GameObject> objectPool;

    private void Start()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < initialSize; i++) 
        {
            objectPool.Add(CreateObj());
        }
    }

    public bool CheckForGameObjectInList(GameObject obj) 
    {
        foreach (GameObject go in objectPool) 
        {
            if (go == obj)
                return true;
        }

        return false;
    }

    public GameObject GetObjectFromPool(Vector3 position)
    {
        GameObject obj = null;

        for (int i = 0; i < objectPool.Count; i++) 
        {
            if (!objectPool[i].activeInHierarchy) 
            {
                obj = objectPool[i];
            }
        }

        if (!obj && willGrow) 
        {
            obj = CreateObj();
            objectPool.Add(obj);
        }

        if (!obj) return null;

        obj.SetActive(true);
        obj.transform.position = position;

        foreach (iPoolable component in obj.GetComponents(typeof(iPoolable))) 
        {
            component.OnUnpool();
        }

        return obj;
    }

    public void ReturnToPool(GameObject obj) 
    {
        if (!obj) return;

        foreach (iPoolable component in obj.GetComponents(typeof(iPoolable))) 
        {
            component.OnPool();
        }

        obj.SetActive(false);
    }

    private GameObject CreateObj()
    {
        GameObject obj = Instantiate(objectToPool);
        obj.SetActive(false);
        return obj;
    }
}
