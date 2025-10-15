using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }
    public GameObject bullet;

    private Queue<GameObject> bullets = new Queue<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        MakePool();
    }

    private void MakePool()
    {
        GameObject tempObj;
        for (int i = 0; i < 25; i++)
        {
            tempObj = bullet;
            tempObj.transform.SetParent(transform);
            bullets.Enqueue(tempObj);
            tempObj.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        if (bullets.Count != 0)
        {
            GameObject obj = bullets.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
        
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        bullets.Enqueue(obj);
    }
}
