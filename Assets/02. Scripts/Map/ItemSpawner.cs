
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("아이템 프리팹들")]
    public GameObject[] itemPrefabs;   // 체리와 보석 아이템

    [Header("스폰 설정")]
    public int itemsPerChunk = 4;                       // 청크마다 스폰할 아이템 개수
    public float checkRadius = 0.5f;                    // 겹침 검사 반경
    public LayerMask wallLayer;                         // 벽 레이어
    public LayerMask itemLayer;                         // 아이템 레이어

    private Vector2 spawnRangeX = new(-3f, 3f);  // X축 랜덤 범위
    private Vector2 spawnRangeY = new(2f, 8f);   // Y축 랜덤 범위

    public void SpawnItems(GameObject chunk)
    {
        int spawned = 0;
        int maxAttempts = 50; // 무한 루프 방지

        while (spawned < itemsPerChunk && maxAttempts > 0)
        {
            maxAttempts--;

            int itemIndex = Random.Range(0, itemPrefabs.Length);
            GameObject itemPrefab = itemPrefabs[itemIndex];

            float randX = Random.Range(spawnRangeX.x, spawnRangeX.y);
            float randY = Random.Range(spawnRangeY.x, spawnRangeY.y);

            Vector3 spawnPos = chunk.transform.position + new Vector3(randX, randY, 0);

            // 1. 벽과 겹치지 않도록 검사
            if (Physics2D.OverlapCircle(spawnPos, checkRadius, wallLayer))
                continue;

            // 2. 다른 아이템과 겹치지 않도록 검사
            if (Physics2D.OverlapCircle(spawnPos, checkRadius, itemLayer))
                continue;

            // 3. 아이템 생성
            GameObject item = Instantiate(itemPrefab, spawnPos, Quaternion.identity, chunk.transform);
            item.layer = LayerMask.NameToLayer("Item"); 
            spawned++;
        }

    }
}
