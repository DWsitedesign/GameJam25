using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerData : MonoBehaviour
{
    public int money = 100;
    public int health = 100;
    public int maxHealth = 100;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public int attackDamage = 10;
    public float attackFrequency = 1f;
    public int numberOfLogs = 0;
    public int numberOfTimber = 0;
    public int maxNumberOfLogs = 15;
    public int logMarketPrice = 10;
    public int timberMarketPrice = 20;
    public TextMeshProUGUI moneyCounter;
    public TextMeshProUGUI timberCounter;
    public TextMeshProUGUI logCounter;
    public Slider healthSlider;

    internal Dictionary<string, int> playerLevels = new Dictionary<string, int>
    {
        //Boots (movement)
        {"boots", 0},
        // truck (health and fuel)
        {"truck", 0},
        // Log storage
        {"logs", 0},
        // 2x4 cutter
        {"milling", 0},
        // hand saw upgrade
        {"handsaw", 0},
        // Axe (damage)
        {"damage", 0},
        // Player Health
        {"health", 0},
    };


    void Start()
    {
        SaveData saveData = FindAnyObjectByType<SaveData>();
        if(saveData){
            saveData.setPlayerData(this);
        }
        depositMoney(0);
            logCounter.text=numberOfLogs.ToString();
            timberCounter.text=numberOfTimber.ToString();
            healthSlider.value=health/maxHealth;

    }

    public int getLevel(string playerAtt)
    {
        return playerLevels[playerAtt];
    }
    public bool levelUp(int value, string playerAtt)
    {
        if (withDrawMoeny(value))
        {
            playerLevels[playerAtt]++;
            reCalcLevels();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void reCalcLevels()
    {
        health = health + 10 * getLevel("health");
        maxHealth = 100 + 10 * getLevel("health");
        walkSpeed = 6f + 2 * getLevel("boots");
        runSpeed = 12f + 2 * getLevel("boots");
        maxNumberOfLogs = 10 + 2 * getLevel("logs");
        attackDamage = 10 + 5*getLevel("damage");
    }

    public bool depositMoney(int value)
    {
        money += value;
        moneyCounter.text = "$" + money;
        return true;
    }

    public bool withDrawMoeny(int value)
    {
        if (value > money)
        {
            return false;
        }
        money -= value;
        moneyCounter.text = "$" + money;
        return true;
    }
    internal void pickupHealth()
    {
        health += 15;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    internal void pickupMoney()
    {
        depositMoney(20);
    }
    internal void addLog()
    {
        numberOfLogs += 1;
        if (numberOfLogs > maxNumberOfLogs)
        {
            numberOfLogs = maxNumberOfLogs;
        }else{
            logCounter.text=numberOfLogs.ToString();
        }

    }

    internal void sellLogs()
    {
        if (numberOfLogs >= 1)
        {
            depositMoney(logMarketPrice);
            numberOfLogs--;
            logCounter.text=numberOfLogs.ToString();
        }

    }

    internal void sellTimber()
    {
        if (numberOfTimber >= 1)
        {
            depositMoney(timberMarketPrice);
            numberOfTimber--;
            timberCounter.text=numberOfTimber.ToString();
        }
    }
    internal void takeDamage(int amount){
        if (health <= 0)
        {
            // todo: make the death splash screen
            FindAnyObjectByType<MenuManager>().playerDeath();
            FindAnyObjectByType<PlayerControls>().isDead=true;
            FindAnyObjectByType<EnemySpawn>().playerDeath();
        }
        else
        {
            health-=amount;
        }
        healthSlider.value=(float)health/maxHealth;
    }
}
