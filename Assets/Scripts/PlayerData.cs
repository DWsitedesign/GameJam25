using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public int money = 100;
    public int health = 100;
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
}
