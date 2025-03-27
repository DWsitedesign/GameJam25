using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuHolder;
    public GameObject deathMenu;
    private bool isDead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuHolder.SetActive(false);
        if (deathMenu)
        {
            deathMenu.SetActive(false);
        }
        // Debug.Log(!menuHolder.activeSelf);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && Input.GetKeyDown(KeyCode.Q))
        {
            if (menuHolder.activeSelf)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }

    void OpenMenu()
    {
        menuHolder.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void CloseMenu()
    {
        menuHolder.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    public void playerDeath()
    {
        isDead = true;
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
