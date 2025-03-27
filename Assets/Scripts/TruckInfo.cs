using System;
using UnityEngine;

public class TruckInfo : MonoBehaviour
{
    public int health = 100;
    public int maxHealth = 100;
    public int fuel = 100;
    public int maxFuel = 100;


    void Start()
    {
        SaveData saveData = FindAnyObjectByType<SaveData>();
        if(saveData){
            saveData.setTruckData(this);
        }
    }
    internal void repair()
    {
        health += 25;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    internal void fueling()
    {
        fuel += 10;
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }
    internal void pickupFuel()
    {
        fuel += 15;
        if (fuel > maxFuel)
        {
            fuel = maxFuel;
        }
    }

    internal bool useFuel()
    {
        if (fuel <= 0)
        {
            return false;
        }
        else
        {
            fuel -= 2;
            // Debug.Log(fuel);
            return true;
        }

    }

    internal void takeDamage(int damage){
        if (health <= 0)
        {
            // todo: make the death splash screen
        }
        else
        {
            health-=damage;
        }
    }

}
