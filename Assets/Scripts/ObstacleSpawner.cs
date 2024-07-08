using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnRate = 2;
    [SerializeField] private float nextSpawnTime;
    
    private void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            SpawnObstacle();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(Random.Range(-4, 4), 0, 20), Quaternion.identity);
    }
}
