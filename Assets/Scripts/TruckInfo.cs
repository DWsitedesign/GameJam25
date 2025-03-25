using System;
using UnityEngine;

public class TruckInfo : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int fuel = 100;
    public int maxFuel = 100;

    internal void repair()
    {
        health+=25;
        if(health>maxHealth){
            health=maxHealth;
        }
    }
    internal void fueling()
    {
        fuel+=10;
        if(fuel>maxFuel){
            fuel=maxFuel;
        }
    }
    internal void pickupFuel(){
        fuel+=15;
        if(fuel>maxFuel){
            fuel=maxFuel;
        }
    }

}
