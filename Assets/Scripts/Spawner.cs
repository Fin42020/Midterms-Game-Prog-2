using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] Enemies;
    private GameObject enemyToSpawn;

    [Header("Floats!")]
    public float MinDistance = 7f;
    public float MaxDistance = 10f;

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(2);
        }
    }

    public void Spawn()
    {
        int randomEnemy = Random.Range(0, Enemies.Length);
        enemyToSpawn = Enemies[randomEnemy];
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        float spawnDistance = Random.Range(MinDistance, MaxDistance);
        Vector2 spawnPosition = randomDirection * spawnDistance;
        Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);
    }
}
