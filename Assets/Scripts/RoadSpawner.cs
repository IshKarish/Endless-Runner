using UnityEngine;
using Random = UnityEngine.Random;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roadPrefab;
    GameObject _previousRoad;

    [SerializeField] private GameObject obstaclePrefab;
    
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private Coin[] coins;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GenerateNewRoad();
        }
    }

    private void GenerateNewRoad()
    {
        Vector3 newPos = transform.parent.position + new Vector3(0, 0, transform.parent.localScale.z * 10);
        GameObject newRoad = Instantiate(roadPrefab, newPos, Quaternion.identity);
        newRoad.GetComponentInChildren<RoadSpawner>()._previousRoad = transform.parent.gameObject;

        GenerateRoadObstacles(newRoad);
        SpawnCoins(newRoad);
        
        if (_previousRoad) Destroy(_previousRoad);
    }

    private void GenerateRoadObstacles(GameObject newRoad)
    {
        float i = 0;
        while (i < transform.parent.localScale.z * 10)
        {
            float randomX = Random.Range(-4, 4);
            
            Vector3 newObstaclePos = newRoad.transform.position + new Vector3(randomX, 0.5f, i);
            Instantiate(obstaclePrefab, newObstaclePos, Quaternion.identity);
            
            i += Random.Range(5, 16);
        }
    }

    private void SpawnCoins(GameObject newRoad)
    {
        float i = 0;
        while (i < transform.parent.localScale.z * 10)
        {
            float randomX = Random.Range(-4, 4);
            float randomY = Random.Range(0.5f, 2.5f);
            
            Vector3 newCoinPos = newRoad.transform.position + new Vector3(randomX, randomY, i);
            GameObject newCoin = Instantiate(coinPrefab, newCoinPos, Quaternion.identity);
            
            newCoin.GetComponentInChildren<CoinSetup>().coin = GetRandomCoin();
            
            i += Random.Range(5, 16);
        }
    }

    Coin GetRandomCoin()
    {
        Coin coin = coins[0];
        float random = Random.Range(0, 1.5f);
        
        if (random < 1) return coin;
        return coins[Random.Range(0, coins.Length)];
    }
}
