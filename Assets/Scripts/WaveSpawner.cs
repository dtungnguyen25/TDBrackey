using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    // Reference to the enemy prefab to be spawned
    public Transform spawnPoint;
    // Reference to the spawn point where the enemy will be spawned
    public float timeBetweenWaves = 5f;
    // Time between each wave of enemies
    private float startCountdown = 2f;
    // Countdown timer for the start of the wave

    public TextMeshProUGUI waveCountdownText;
    // Reference to the UI text element to display the countdown timer
    private int waveIndex = 0;
    // Index of the current wave

    void Update()
    {
        if (startCountdown <= 0f)
        // Check if the countdown has reached zero
        {
            StartCoroutine(SpawnWave());
            // Start the coroutine to spawn the wave of enemies
            startCountdown = timeBetweenWaves;
            // Reset the countdown timer for the next wave
        }

        startCountdown -= Time.deltaTime;
        // Decrease the countdown timer by the time since the last frame
        waveCountdownText.text = Mathf.Round(startCountdown).ToString();
        // Update the UI text element with the rounded countdown timer value
    }

    IEnumerator SpawnWave()
    {
        
        waveIndex++;
        // Increment the wave index to indicate the next wave
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            // Call the method to spawn an enemy
            yield return new WaitForSeconds(0.5f);
            // Wait for a short duration before spawning the next enemy
        }

    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab.gameObject, spawnPoint.position, spawnPoint.rotation);
        // Instantiate the enemy prefab at the spawn point's position and rotation
    }

}
