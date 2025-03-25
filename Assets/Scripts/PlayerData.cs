using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int money = 100;
    public int health = 100;
    public int maxHealth = 100;
    public TextMeshProUGUI moneyCounter;

    public bool depositMoney(int value){
        money+=value;
        moneyCounter.text="$"+money;
        return true;
    }

    public bool withDrawMoeny(int value){
        if (value>money){
            return false;
        }
        money-=value;
        moneyCounter.text="$"+money;
        return true;
    }
    internal void pickupHealth(){
        health+=15;
        if(health>maxHealth){
            health=maxHealth;
        }
    }
    internal void pickupMoney(){
        depositMoney(20);
    }
}
