using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Основные настройки")]
    public GameObject enemyPrefab;     // Префаб врага
    public Transform player;           // Ссылка на игрока
    public float spawnRadius = 10f;    // Радиус спавна вокруг игрока
    public float spawnInterval = 2f;   // Интервал спавна (сек)
    public int maxEnemies = 20;        // Лимит живых врагов
    public bool spawnEnabled = true;

    [Header("Усложнение (по желанию)")]
    public float difficultyIncreaseRate = 0.95f; // чем меньше, тем быстрее спавн
    public float minSpawnInterval = 0.5f;

    private int currentEnemies = 0;

    void Start()
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").transform;

        StartCoroutine(SpawnLoop());
    }

    IEnumerator SpawnLoop()
    {
        while (spawnEnabled && GameManager.Instance.isPlayable)
        {
            if (currentEnemies < maxEnemies)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);

            // Немного ускоряем спавн с течением времени
            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval * difficultyIncreaseRate);
        }
    }

    void SpawnEnemy()
    {
        Vector2 spawnPos = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 worldPos = player.position + new Vector3(spawnPos.x, spawnPos.y, 0f);

        GameObject enemy = Instantiate(enemyPrefab, worldPos, Quaternion.identity);
        Enemy enemyScript = enemy.GetComponent<Enemy>();
        enemyScript.OnDeath += HandleEnemyDeath;
        enemyScript.target = player;
        currentEnemies++;
    }

    void HandleEnemyDeath(Enemy e)
    {
        currentEnemies--;
        GameManager.Instance.score += 1;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player ? player.position : transform.position, spawnRadius);
    }
}