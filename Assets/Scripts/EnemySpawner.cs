using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]
    List<Waveconfig> waveconfigs;

    [SerializeField]
    int startingWave = 0;

    [SerializeField] bool looping = false;
    // Use this for initialization
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpanwnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpanwnAllWaves()
    {
        for (var i = startingWave; i < waveconfigs.Count; i++)
        {
            var curentWave = waveconfigs[i];

            yield return StartCoroutine(SpawnAllEnemiesInWave(curentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(Waveconfig curentWave)
    {
        for (var i = 0; i < curentWave.GetNumberOfEnemy(); i++)
        {
            var newEnemy = Instantiate(curentWave.GetEnemyPrefab(), curentWave.GetWaypoints()[0].transform.position, Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(curentWave);

            yield return new WaitForSeconds(curentWave.GetTimeBetweenSpawn());
        }
    }
}
