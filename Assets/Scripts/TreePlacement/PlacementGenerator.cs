using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

public class PlacementGenerator : MonoBehaviour
{
    [SerializeField] GenerateObject[] generateObjects;
    [Header("Raycast Settings")]
    // [SerializeField] int density;
    [Space]
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;
    [SerializeField] Vector2 xRange;
    [SerializeField] Vector2 zRange;
    // [SerializeField] float heightOffGround = 0;
    [Header("Prefab Variation Settings")]
    [SerializeField, Range(0, 1)] float rotateTowardsNormal;
    [SerializeField] Vector2 rotationRange;
    // [SerializeField] Vector2 planeScale;
    // [SerializeField] Vector2 yScale;
    public bool autoUpdate = true;

    public int visibleDistanceX;
    public int visibleDistanceZBack;
    public int visibleDistanceZForward;

    private Dictionary<Vector2, GameObject> terrainObjectDictionary = new Dictionary<Vector2, GameObject>();

    private List<GameObject> terrainObjectsVisibleLastUpdate = new List<GameObject>();
    private List<GameObject> terrainObjectsVisibleLastUpdateCp = new List<GameObject>();


    void Start()
    {
        Generate();
    }

    public void CheckActive(Vector2 position)
    {
        terrainObjectsVisibleLastUpdateCp = new List<GameObject>(terrainObjectsVisibleLastUpdate);
        terrainObjectsVisibleLastUpdate.Clear();


        // terrainObjectsVisibleLastUpdate.Clear();

        for (int yOffset = -visibleDistanceZBack; yOffset <= visibleDistanceZForward; yOffset++)
        {
            for (int xOffset = -visibleDistanceX; xOffset <= visibleDistanceX; xOffset++)
            {

                Vector2 viewableCoord = new Vector2(position.x + xOffset, position.y + yOffset);
                if (terrainObjectDictionary.ContainsKey(viewableCoord))
                {
                    terrainObjectsVisibleLastUpdate.Add(terrainObjectDictionary[viewableCoord]);
                    terrainObjectDictionary[viewableCoord].SetActive(true);

                }
            }
        }

        for (int i = 0; i < terrainObjectsVisibleLastUpdateCp.Count; i++)
        {
            if(!terrainObjectsVisibleLastUpdate.Contains(terrainObjectsVisibleLastUpdateCp[i])){
            terrainObjectsVisibleLastUpdateCp[i].SetActive(false);
            }
        }
        terrainObjectsVisibleLastUpdateCp.Clear();
    }

    public void RemoveObject(GameObject genObject){
        terrainObjectsVisibleLastUpdate.Remove(genObject);
        terrainObjectDictionary.Remove(new Vector2(genObject.transform.position.x, genObject.transform.position.z));
        Destroy(genObject);
    }

    public void Generate()
    {
        Clear();

        for (int l = 0; l < generateObjects.Length; l++)
        {
            

        for (int i = 0; i < generateObjects[l].density; i++)
        {
            float sampleX = Mathf.RoundToInt(UnityEngine.Random.Range(xRange.x, xRange.y));
            float sampleY = Mathf.RoundToInt(UnityEngine.Random.Range(zRange.x, zRange.y));
            Vector3 rayStart = new Vector3(sampleX, maxHeight, sampleY);



            if (!Physics.Raycast(rayStart, Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                continue;

            }

            if (hit.point.y < minHeight)
                continue;

            Vector2 spotCoord = new Vector2(hit.point.x,hit.point.z);
            if(terrainObjectDictionary.ContainsKey(spotCoord)){
                i--;
                continue;
            }


            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(generateObjects[l].prefabs[UnityEngine.Random.Range(0, generateObjects[l].prefabs.Length)], transform);
            instantiatedPrefab.transform.position = hit.point + Vector3.up * generateObjects[l].heightOffGround;
            instantiatedPrefab.transform.Rotate(Vector3.up, UnityEngine.Random.Range(rotationRange.x, rotationRange.y), Space.Self);
            // instantiatedPrefab.transform.rotation=Quaternion.Lerp(transform.rotation,transform.rotation*Quaternion.FromToRotation(instantiatedPrefab.transform.up,hit.normal),rotateTowardsNormal);
            float planeScaler = UnityEngine.Random.Range(generateObjects[l].planeScale.x, generateObjects[l].planeScale.y);
            instantiatedPrefab.transform.localScale = new Vector3(
                planeScaler,
                UnityEngine.Random.Range(generateObjects[l].yScale.x, generateObjects[l].yScale.y),
                planeScaler
            );
            instantiatedPrefab.SetActive(false);
            terrainObjectDictionary.Add(spotCoord, instantiatedPrefab);

        }
        }
    }
    public void Clear()
    {
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}

[System.Serializable]
public struct GenerateObject{
    public GameObject[] prefabs;
    public int density;
    public float heightOffGround;
    public Vector2 planeScale;
    public Vector2 yScale;
}
