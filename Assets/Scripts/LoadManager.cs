using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{

    public SaveData saveData;

    void Start()
    {
        saveData=FindAnyObjectByType<SaveData>();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
    public void MainMenu()
    {   
        saveData.resetData();
        SceneManager.LoadScene(0);
    }
    public void LoadForest()
    {
        saveData.getData();
        SceneManager.LoadScene(2);
    }
    public void NewGame()
    {
        saveData.resetData();
        SceneManager.LoadScene(1);
    }
    public void LoadHome()
    {
        saveData.getData();
        SceneManager.LoadScene(1);
    }
    public void LoadShop()
    {
        saveData.getData();
        SceneManager.LoadScene(3);
    }
    public void death(){
        SceneManager.LoadScene(1);
    }
}
