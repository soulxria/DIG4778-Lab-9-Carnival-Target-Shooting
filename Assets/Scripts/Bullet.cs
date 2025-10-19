using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Coroutine despawnCoroutine;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        despawnCoroutine = StartCoroutine(DespawnAfterTimer());
    }

    void OnDisable()
    {
        if (despawnCoroutine != null)
        {
            StopCoroutine(despawnCoroutine);
            despawnCoroutine = null;
        }

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }   

    IEnumerator DespawnAfterTimer()
    {
        yield return new WaitForSeconds(20f);
        ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ReturnToPool();
        Debug.Log("Bullet Hit Something!");
    }

    private void ReturnToPool()
    {
        if (gameObject.activeSelf)
        {
            ObjectPooler.Instance.ReturnBullet(this.gameObject);
        }
    }
}
