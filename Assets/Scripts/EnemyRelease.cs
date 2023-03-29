using UnityEngine;

public class EnemyRelease : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] Enemy enemyPrefab; 
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnDelay; 

    [Space]
    [Header("Enemy Attributes")]
    [SerializeField] int hp = 99;
    [SerializeField] float speed = 5;
    [SerializeField] Vector3 scale = new Vector3(5f, 5f, 5f);

    void Awake() => SetGiantAttributes();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            Invoke("SpawnEnemy", spawnDelay);
    }

    void SetGiantAttributes()
    {
        if (enemyPrefab != null)
        {
            EnemyChallenge enemyChallenge = enemyPrefab.GetComponent<EnemyChallenge>();
            if (enemyChallenge != null) enemyChallenge.HP = hp;
            enemyPrefab.MoveSpeed = speed;
            enemyPrefab.transform.localScale = scale;
        }
    }

    void SpawnEnemy() => Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
}
