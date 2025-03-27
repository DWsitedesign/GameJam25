using UnityEngine;
using UnityEditor;

public class TreeFall : MonoBehaviour
{

    private Quaternion lastRotate;
    private Vector3 directionToStick;
    private bool spawnedLogs = false;


    void Update()
    {

        directionToStick = transform.position - FindFirstObjectByType<PlayerControls>().transform.position;
        directionToStick.y = 0;
        Quaternion rotation = Quaternion.AngleAxis(-90f, Vector3.Cross(directionToStick, Vector3.up));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 90 * Time.deltaTime);
        if (rotation == lastRotate && !spawnedLogs)
        {
            spawnedLogs=true;
            Invoke("spawnLogs", 1f);

        }

        lastRotate = rotation;

    }

    void spawnLogs()
    {
        GameObject prefabLog = Resources.Load<GameObject>("Log, Hollow");
        GameObject instantiatedPrefab;
        for (int i = 0; i <= Random.Range(0, 2); i++)
        {
            instantiatedPrefab = Instantiate(prefabLog);
            instantiatedPrefab.transform.position = transform.position + directionToStick.normalized * (2 * (i + 1)) + directionToStick.normalized * (i * 3);
            instantiatedPrefab.transform.Rotate(Vector3.up, transform.rotation.eulerAngles.y, Space.Self);
        }

        PlacementGenerator generators = FindAnyObjectByType<PlacementGenerator>();
        generators.RemoveObject(gameObject);
    }
}
