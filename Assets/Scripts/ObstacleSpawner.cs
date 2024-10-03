using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float startTime = 3;
    [SerializeField] private float spawnTime = 3;

    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnTime);
    }

    private void Spawn()
    {
        int randomId = Random.Range(0, ObjectPool.Instance.GetPoolCount());
        GameObject obstacle = ObjectPool.Instance.GetObject(randomId, transform.position);
    }
}