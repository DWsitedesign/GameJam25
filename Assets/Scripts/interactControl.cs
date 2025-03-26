using System;
using UnityEngine;

public class interactControl : MonoBehaviour
{
    [SerializeField]
    public string message;

    [SerializeField]
    public GameObject callBackHolder;

    public enum callBackTypes
    {
        truckRepair, truckFuel, truckLog, goHome, goForest, goShop, pickupFuel, pickupHealth, pickupMoney, chopTree, pickupLog,
        sellLog, sellTimber, upgrade

    }

    public callBackTypes callBackType;

    public string messageText()
    {
        return message;
    }
    public Action callBack()
    {
        if (callBackType == callBackTypes.goHome)
        {
            return GameObject.FindWithTag("Loader").GetComponent<LoadManager>().LoadHome;
        }
        else if (callBackType == callBackTypes.goForest)
        {
            return GameObject.FindWithTag("Loader").GetComponent<LoadManager>().LoadForest;
        }
        else if (callBackType == callBackTypes.goShop)
        {
            return GameObject.FindWithTag("Loader").GetComponent<LoadManager>().LoadShop;
        }
        else if (callBackType == callBackTypes.truckFuel)
        {
            return callBackHolder.GetComponent<TruckInfo>().fueling;
        }
        else if (callBackType == callBackTypes.truckRepair)
        {
            return callBackHolder.GetComponent<TruckInfo>().repair;
        }
        else if (callBackType == callBackTypes.truckLog)
        {
            return GameObject.FindWithTag("Player").GetComponent<PlayerControls>().depositLog;
        }
        else if (callBackType == callBackTypes.pickupFuel)
        {
            return GameObject.FindFirstObjectByType<TruckInfo>().pickupFuel;
        }
        else if (callBackType == callBackTypes.pickupHealth)
        {
            return GameObject.FindFirstObjectByType<PlayerData>().pickupHealth;
        }
        else if (callBackType == callBackTypes.pickupMoney)
        {
            return GameObject.FindFirstObjectByType<PlayerData>().pickupMoney;
        }
        else if (callBackType == callBackTypes.chopTree)
        {
            return () =>
            {
                gameObject.AddComponent<TreeFall>();
            };
        }
        else if (callBackType == callBackTypes.pickupLog)
        {
            return () =>
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerControls>().PickupObj(gameObject);
            };
        }
        else if (callBackType == callBackTypes.sellLog)
        {
            return () =>
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerData>().sellLogs();
            };
        }
        else if (callBackType == callBackTypes.sellTimber)
        {
            return () =>
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerData>().sellTimber();
            };
        }else if (callBackType == callBackTypes.upgrade)
        {
            return () =>
            {
                gameObject.GetComponent<LevelUpScript>().levelUp();
            };
        }


        return () => Debug.Log("no function");

    }
}
