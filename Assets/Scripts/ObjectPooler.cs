using UnityEngine;
using System.Collections.Generic;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance { get; private set; }
    public GameObject bulletPrefab;

    private Queue<GameObject> bullets = new Queue<GameObject>();

    [SerializeField] private int bulletPoolSize;

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
        MakeBulletPool();
    }

    private void MakeBulletPool()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject tempObj = Instantiate(bulletPrefab);
            tempObj.transform.SetParent(transform);
            bullets.Enqueue(tempObj);
            tempObj.SetActive(false);
        }
    }

    public GameObject GetBullet()
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

    public void ReturnBullet(GameObject obj)
    {
        obj.SetActive(false);
        bullets.Enqueue(obj);
    }
}
