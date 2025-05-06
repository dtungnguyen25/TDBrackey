using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float startCountdown = 2f;

    public Text waveCountdownText;
    private int waveIndex = 0;

    void Update()
    {
        if (startCountdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            startCountdown = timeBetweenWaves;
        }

        startCountdown -= Time.deltaTime;

        waveCountdownText.text = Mathf.Round(startCountdown).ToString();
    }

    IEnumerator SpawnWave()
    {
        
        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

}
