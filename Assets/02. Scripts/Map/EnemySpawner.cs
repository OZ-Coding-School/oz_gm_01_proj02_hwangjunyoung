
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("몬스터 프리팹들")]
    public GameObject[] enemyPrefabs;                   // 사용할 적 프리팹들

    [Header("스폰 설정")]
    public int enemiesPerChunk = 3;                     // 청크마다 스폰할 몬스터 개수
    public float minDistance = 1.5f;                    // 몬스터끼리 최소 거리
    public float checkRadius = 0.5f;                    // 벽 충돌 검사 반경

    [Header("레이어 설정")]
    public LayerMask wallLayer;       // 벽 레이어
    public LayerMask enemyLayer;      // 적 레이어

    private Vector2 spawnRangeX = new(-3f, 3f);         // X축 랜덤 범위
    private Vector2 spawnRangeY = new(2f, 8f);          // Y축 랜덤 범위

    public void SpawnEnemies(GameObject chunk)
    {
        int spawned = 0;
        int maxAttempts = 50; // 무한 루프 방지

        while (spawned < enemiesPerChunk && maxAttempts > 0)
        {
            maxAttempts--;

            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            GameObject enemyPrefab = enemyPrefabs[enemyIndex];

            float randX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randY = Random.Range(spawnRangeY.x, spawnRangeY.y);

            Vector3 spawnPos = chunk.transform.position + new Vector3(randX, randY, 0);

            // 1. 벽과 겹치는지 검사
            if (Physics2D.OverlapCircle(spawnPos, checkRadius, wallLayer))
                continue;

            // 2. 다른 적과 겹치는지 검사
            if (Physics2D.OverlapCircle(spawnPos, minDistance, enemyLayer))
                continue;

            // 3. 스폰
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, chunk.transform);
            enemy.layer = LayerMask.NameToLayer("Enemy"); 
            spawned++;
        }
    }
}