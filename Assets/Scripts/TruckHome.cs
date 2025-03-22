using TMPro;
using UnityEngine;

public class TruckHome : MonoBehaviour
{
    private string currentName;
    private bool isInTrigger=false;
    public PlayerData playerInfo;
    public TruckInfo truckInfo;
    private TextMeshProUGUI userNote;

    void Start()
    {
        currentName = gameObject.name;
        userNote = GameObject.FindWithTag("UserNotifications").GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E)){
            if (currentName.Equals("Repairs") && truckInfo.health!=truckInfo.maxHealth && playerInfo.withDrawMoeny(25))
            {
                
                userNote.text = "Repaired";
                truckInfo.repair();

            }
            else if (currentName.Equals("Fuel") && truckInfo.fuel!=truckInfo.maxFuel && playerInfo.withDrawMoeny(9))
            {
                truckInfo.fueling();
                userNote.text = "Hold E to fuel\n$3";
            }
            else if (currentName.Equals("Leave"))
            {
                Debug.Log("leave");
                userNote.enabled = true;
                userNote.text = "Hold E to DEPART";
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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
                userNote.text = "Hold E to fuel\n$3";
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
        if (other.gameObject.CompareTag("Player"))
        {
            isInTrigger=false;
            TextMeshProUGUI userNote = GameObject.FindWithTag("UserNotifications").GetComponent<TextMeshProUGUI>();
            userNote.enabled = false;
        }
    }
}
