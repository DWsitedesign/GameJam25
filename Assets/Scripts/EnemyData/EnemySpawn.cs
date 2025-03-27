using UnityEditor;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform playerPos;
    public Transform truckPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("spawnEnemiesControl", 0, 4f);
    }


    internal void playerDeath()
    {
        CancelInvoke("spawnEnemiesControl");
    }
    void spawnEnemiesControl()
    {
        spawnEnemies(Random.Range(1, 11));

    }
    void spawnEnemies(int amount)
    {
        Vector2 xRange = new Vector2(playerPos.transform.position.x-40, playerPos.transform.position.x+40);
        Vector2 yRange = new Vector2(playerPos.transform.position.z-40, playerPos.transform.position.z+40);
        for (int i = 0; i < amount; i++)
        {
            
            // Debug.Log((yRange.x, yRange.y));
            float sampleX = Random.Range(xRange.x, xRange.y);
            float sampleY = Random.Range(yRange.x,yRange.y);
            Vector3 rayStart = new Vector3(sampleX, 10000, sampleY);
            // Debug.Log(rayStart);
            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity,LayerMask.GetMask("Ground")))
            {
                continue;
            }

            GameObject instantiatedPrefab = Instantiate(enemyPrefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            // Debug.Log(hit.point);
            // Debug.Log(instantiatedPrefab.transform.position);
            instantiatedPrefab.transform.localScale = Vector3.one;
            EnemyControler enemyControler = instantiatedPrefab.GetComponent<EnemyControler>();
            enemyControler.player=playerPos;
            enemyControler.truck=truckPos;
        }
    }
}
