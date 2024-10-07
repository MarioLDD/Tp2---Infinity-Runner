using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float startTime = 3;
    [SerializeField] private float minTime = 3;
    [SerializeField] private float maxTime = 3;
    private float spawnTime;

    void Start()
    {
        StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(startTime);

        while(true)
        {
            Spawn();

            spawnTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(spawnTime);
        }
    }

    private void Spawn()
    {
        int randomId = Random.Range(0, ObjectPool.Instance.GetPoolCount());
        GameObject obstacle = ObjectPool.Instance.GetObject(randomId, transform.position);
    }
}