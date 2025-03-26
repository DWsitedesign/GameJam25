using UnityEngine;
using UnityEditor;

public class TruckPlacement : MonoBehaviour
{
    [SerializeField] GameObject prefabTruck;
    [SerializeField] Transform playerTransform;
    private TruckInfo truckInfo;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!Physics.Raycast(new Vector3(0,100,0), Vector3.down, out RaycastHit hit, Mathf.Infinity))
            {
                return;
            }

            GameObject instantiatedPrefab = (GameObject)PrefabUtility.InstantiatePrefab(prefabTruck, transform);
            instantiatedPrefab.transform.position = hit.point;
            instantiatedPrefab.transform.Rotate(Vector3.up, UnityEngine.Random.Range(0, 360), Space.Self);
            // instantiatedPrefab.transform.rotation=Quaternion.Lerp(transform.rotation,transform.rotation*Quaternion.FromToRotation(instantiatedPrefab.transform.up,hit.normal),rotateTowardsNormal);
            instantiatedPrefab.transform.localScale = Vector3.one;
            instantiatedPrefab.GetComponent<TruckModelSelector>().insertTruckModel(instantiatedPrefab.GetComponentInChildren<TruckInfo>().health);
            truckInfo=instantiatedPrefab.GetComponentInChildren<TruckInfo>();

        if (!Physics.Raycast(new Vector3(0,100,-15), Vector3.down, out RaycastHit playerHit, Mathf.Infinity))
            {
                return;
            }

            playerTransform.transform.position = playerHit.point;

        InvokeRepeating("drainTruckFuel",1f,1f);

        
    }

    void drainTruckFuel()
    {
        if(truckInfo.useFuel()){
            CancelInvoke("truckInfo.useFuel");
        }
    }


}
