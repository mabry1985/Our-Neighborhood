using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    [SerializeField]
    float spawnTimer;
    [SerializeField]
    float spawnRadius;
    [SerializeField]
    int spawnCount;
    [SerializeField]
    int maxEnemiesSpawned;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {   
        while (true)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                Vector3 randomPoint = RandomNavSphere(transform.position, spawnRadius/2, -1);
                Instantiate(enemyPrefab, randomPoint, transform.rotation);
            }
            yield return new WaitForSeconds(spawnTimer);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);    
    }
}
