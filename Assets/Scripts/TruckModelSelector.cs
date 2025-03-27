using UnityEditor;
using UnityEngine;

public class TruckModelSelector : MonoBehaviour
{
    [SerializeField] public bool autoRun=false;
    [SerializeField] public truckData[] truckModels;

    void Awake()
    {
        if (autoRun)
        {
            insertTruckModel(gameObject.GetComponentInChildren<TruckInfo>().health);
        }
    }
    public void insertTruckModel(int health)
    {
        for (int i = 0; i < truckModels.Length; i++)
        {
            if (truckModels[i].minHealth <= health)
            {
                GameObject truckModel = Instantiate(truckModels[i].truckPrefab, transform);
                truckModel.transform.parent = transform;

                return;
            }
        }
    }


}

[System.Serializable]
public struct truckData
{
    public GameObject truckPrefab;
    public int minHealth;
}
