using System.Collections;
using UnityEngine;

/// <summary>
/// Gestiona oleadas de enemigos:
/// - Spawnea X enemigos.
/// - Espera a que no queden enemigos vivos.
/// - Cuenta atrás entre oleadas.
/// - Actualiza el HUD.
/// </summary>
public class WaveManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private HUDWave hudWave;

    [Header("Wave Settings")]
    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private int enemiesFirstWave = 3;
    [SerializeField] private int enemiesIncreasePerWave = 2;
    [SerializeField] private float spawnInterval = 0.3f;

    private int currentWave = 0;
    private bool isSpawning = false;

    /// <summary>
    /// Arranca el bucle de oleadas al iniciar.
    /// </summary>
    private void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("[WaveManager] Enemy Prefab no asignado.");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("[WaveManager] No hay Spawn Points asignados.");
            return;
        }

        StartCoroutine(WaveLoop());
    }

    /// <summary>
    /// Bucle principal:
    /// 1) Cuenta atrás
    /// 2) Spawnea oleada
    /// 3) Espera a que no queden enemigos
    /// 4) Repite
    /// </summary>
    private IEnumerator WaveLoop()
    {
        while (true)
        {
            // 1) Espera entre oleadas con cuenta atrás en HUD
            yield return StartCoroutine(WaveCountdown(timeBetweenWaves));

            // 2) Siguiente oleada
            currentWave++;
            hudWave?.SetWave(currentWave);

            int enemiesThisWave = enemiesFirstWave + (currentWave - 1) * enemiesIncreasePerWave;

            // 3) Spawnear enemigos
            yield return StartCoroutine(SpawnWave(enemiesThisWave));

            // 4) Esperar a que no queden enemigos en escena
            yield return StartCoroutine(WaitUntilNoEnemies());
        }
    }

    /// <summary>
    /// Cuenta atrás antes de iniciar la siguiente oleada, mostrando mensaje en HUD.
    /// </summary>
    private IEnumerator WaveCountdown(float seconds)
    {
        float t = seconds;

        while (t > 0f)
        {
            hudWave?.SetStatus($"Next wave in {Mathf.CeilToInt(t)}...");
            t -= Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Spawnea una oleada de N enemigos, uno cada spawnInterval segundos.
    /// </summary>
    private IEnumerator SpawnWave(int enemyCount)
    {
        isSpawning = true;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnOneEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }

    /// <summary>
    /// Instancia un enemigo en un spawn point aleatorio.
    /// </summary>
    private void SpawnOneEnemy()
    {
        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, sp.position, Quaternion.identity);
    }

    /// <summary>
    /// Espera hasta que no queden enemigos con Tag "Enemy" en escena.
    /// </summary>
    private IEnumerator WaitUntilNoEnemies()
    {
        // Mientras haya enemigos o se esté spawneando, esperar
        while (isSpawning || GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(0.2f);
        }
    }
}
