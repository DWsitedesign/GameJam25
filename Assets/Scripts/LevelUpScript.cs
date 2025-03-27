using UnityEngine;

public class LevelUpScript : MonoBehaviour
{
    public GameObject[] levels;
    public enum levelAttribute
    {
        boots, truck, logs, milling, handsaw, damage, health
    };
    public levelAttribute levelAtt;
    public int levelCost;
    private int level = 0;
    private PlayerData playerData;

    void Start()
    {
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        // Debug.Log(playerData);
        level = playerData.getLevel(levelAtt.ToString());
        for (int i = 0; i < levels.Length; i++)
        {
            levels[i].SetActive(i < level);
        }
    }

    internal void levelUp()
    {
        if (playerData.levelUp(levelCost, levelAtt.ToString()))
        {
            level=playerData.getLevel(levelAtt.ToString());
            if(level-1<levels.Length)
                levels[level-1].SetActive(true);
        }
    }

}
