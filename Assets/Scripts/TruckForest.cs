using TMPro;
using UnityEngine;

public class TruckForest : MonoBehaviour
{
    private string currentName;
    private bool isInTrigger=false;
    public PlayerData playerInfo;
    public TruckInfo truckInfo;
    private TextMeshProUGUI userNote;
    private LoadManager loadManager;

    void Start()
    {
        loadManager = GameObject.FindWithTag("Loader").GetComponent<LoadManager>();
        currentName = gameObject.name;
        userNote = GameObject.FindWithTag("UserNotifications").GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        // if (isInTrigger && Input.GetKeyDown(KeyCode.E)){
        //     if (currentName.Equals("Repairs") && truckInfo.health!=truckInfo.maxHealth && playerInfo.withDrawMoeny(25))
        //     {
                
        //         userNote.text = "Repaired";
        //         truckInfo.repair();

        //     }
        //     else if (currentName.Equals("Fuel") && truckInfo.fuel!=truckInfo.maxFuel && playerInfo.withDrawMoeny(9))
        //     {
        //         truckInfo.fueling();
        //         userNote.text = "Press E to fuel\n$3";
        //     }
        //     else if (currentName.Equals("Leave"))
        //     {
        //         loadManager.LoadHome();
        //     }
        // }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            isInTrigger=true;
            // player is there
            if (currentName.Equals("Repairs"))
            {
                Debug.Log("repair");
                userNote.enabled = true;
                userNote.text = "Hold E to repair\n$25";

            }
            else if (currentName.Equals("Fuel"))
            {
                Debug.Log("fuel");
                userNote.enabled = true;
                userNote.text = "Press E to fuel\n$3";
            }
            else if (currentName.Equals("Leave"))
            {
                Debug.Log("leave");
                userNote.enabled = true;
                userNote.text = "Hold E to DEPART";
            }

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            isInTrigger=false;
            userNote.enabled = false;
        }
    }
}
