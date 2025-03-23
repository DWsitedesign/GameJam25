using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject menuHolder;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuHolder.SetActive(false);
        // Debug.Log(!menuHolder.activeSelf);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
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
}
