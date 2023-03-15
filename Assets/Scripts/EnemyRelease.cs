using UnityEngine;

public class EnemyRelease : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] EnemyChallenge enemyPrefab; 
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay;
    [Space]
    [Header("Enemy Attributes")]
    [SerializeField] float hp;
    [SerializeField] float speed;
    [SerializeField] Vector3 scale;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Invoke("SpawnEnemy", spawnDelay);
    }

    void SpawnEnemy()
    {
        enemyPrefab.MoveSpeed = speed;
        enemyPrefab.transform.localScale = scale;
        Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
