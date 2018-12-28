using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class Waveconfig : ScriptableObject
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject pathPrefab;
    [SerializeField] float timeBetweenSpawn = 0.5f;
    [SerializeField] float spawnRandomFactor = 0.3f;
    [SerializeField] int numberOfEnemy = 5;
    [SerializeField] float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return enemyPrefab; }

    public List<Transform> GetWaypoints()
    {
        var waypoints = new List<Transform>();

        foreach (Transform child in pathPrefab.transform)
        {
            waypoints.Add(child);
        }

        return waypoints;
    }

    public float GetTimeBetweenSpawn() { return timeBetweenSpawn; }

    public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemy() { return numberOfEnemy; }

    public float GetmoveSpeed() { return moveSpeed; }
}
