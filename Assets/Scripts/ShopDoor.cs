using UnityEngine;
using TMPro;
using System;


public class ShopDoor : MonoBehaviour
{
    private bool isInTrigger = false;
    private TextMeshProUGUI userNote;
    private LoadManager loadManager;
    private String currentName;

    void Start()
    {
        loadManager = GameObject.FindWithTag("Loader").GetComponent<LoadManager>();
        userNote = GameObject.FindWithTag("UserNotifications").GetComponent<TextMeshProUGUI>();
        currentName = gameObject.name;

    }

    void Update()
    {
        if (isInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (currentName.Equals("Exit"))
            {
                loadManager.LoadHome();
            }
            else
            {
                loadManager.LoadShop();

            }

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInTrigger = true;
            userNote.enabled = true;
            if (currentName.Equals("Exit"))
            {
                userNote.text = "Hold E to Exit Shop";

            }
            else
            {
                userNote.text = "Hold E to Enter Shop";

            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInTrigger = false;
            userNote.enabled = false;
        }
    }
}
