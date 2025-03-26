using UnityEditor;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{

    public GameObject enemyPrefab;
    public Transform playerPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        spawnEnemies(3);
    }

    void spawnEnemies(int amount){

        float sampleX = Mathf.RoundToInt(UnityEngine.Random.Range(-20,20));
            float sampleY = Mathf.RoundToInt(UnityEngine.Random.Range(-20,20));
            Vector3 rayStart = new Vector3(sampleX, 100, sampleY);
        if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(enemyPrefab, transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.localScale = Vector3.one;
    }
}
