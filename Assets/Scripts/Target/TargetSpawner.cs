using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] public GameObject targetPrefab;
    [SerializeField] private GameObject smallTargetSpawn;
    [SerializeField] private GameObject mediumTargetSpawn;
    [SerializeField] private GameObject largeTargetSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(LargeSpawnRoutine());
        StartCoroutine(MediumSpawnRoutine());
        StartCoroutine(SmallSpawnRoutine());
    }

    IEnumerator SmallSpawnRoutine()
    {
        yield return new WaitForSeconds(8f);

        while (true)
        {
            SpawnSmallTarget();
            yield return new WaitForSeconds(6f);
        }
    }

    IEnumerator MediumSpawnRoutine()
    {
        yield return new WaitForSeconds(6f);

        while (true)
        {
            SpawnMediumTarget();
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator LargeSpawnRoutine()
    {
        yield return new WaitForSeconds(4f);

        while (true)
        {
            SpawnLargeTarget();
            yield return new WaitForSeconds(4f);
        }
    }

    void SpawnSmallTarget()
    {
        Vector3 spawnPosition = smallTargetSpawn.transform.position;
        TargetBuilder smallTarget = new TargetBuilder.Builder(targetPrefab)
            .WithSpeed(3f)
            .WithPointValue(500)
            .WithSize(new Vector3(0.6f, 0.6f, 0.6f))
            .WithColor(Color.blue)
            .Build();

        smallTarget.transform.position = spawnPosition;
    }

    void SpawnMediumTarget()
    {
        Vector3 spawnPosition = mediumTargetSpawn.transform.position;
        TargetBuilder mediumTarget = new TargetBuilder.Builder(targetPrefab)
            .WithSpeed(2f)
            .WithPointValue(250)
            .WithSize(new Vector3(0.8f, 0.8f, 0.8f))
            .WithColor(Color.yellow)
            .Build();

        mediumTarget.transform.position = spawnPosition;
    }

    void SpawnLargeTarget()
    {
        Vector3 spawnPosition = largeTargetSpawn.transform.position;
        TargetBuilder largeTarget = new TargetBuilder.Builder(targetPrefab)
            .WithSpeed(1f)
            .WithPointValue(100)
            .WithSize(Vector3.one)
            .WithColor(Color.red)
            .Build();

        largeTarget.transform.position = spawnPosition;
    }
}
