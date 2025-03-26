using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuControl : MonoBehaviour
{
    private GameObject mainButtons;
    private GameObject creditSection;
    private GameObject optionSection;
    void Start()
    {
        mainButtons = transform.Find("ButtonHolder").gameObject;
        optionSection = transform.Find("Options").gameObject;
        creditSection = transform.Find("Credits").gameObject;

        ResetMenu();
    }
    public void menuOptions(){
        mainButtons.SetActive(false);
        optionSection.SetActive(true);

    }
    public void menuCredits(){
mainButtons.SetActive(false);
        creditSection.SetActive(true);
    }
    public void menuStart(){
        SceneManager.LoadScene(1);
    }

    public void ResetMenu(){
        mainButtons.SetActive(true);
        creditSection.SetActive(false);
        optionSection.SetActive(false);
    }

    public void menuQuit(){
        Debug.Log("Quitting the game...");
        Application.Quit();
    }
}
