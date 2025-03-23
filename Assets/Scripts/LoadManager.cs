using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{


    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadForest()
    {
        SceneManager.LoadScene(2);
    }
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadHome()
    {
        SceneManager.LoadScene(1);
    }
    public void LoadShop()
    {
        SceneManager.LoadScene(3);
    }
}
