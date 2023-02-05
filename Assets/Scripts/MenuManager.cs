using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject CreditsMenu;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        OpenMain();
    }

    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void OpenMain()
    {
        MainMenu.SetActive(true);
        CreditsMenu.SetActive(false);
    }

    public void OpenCredits()
    {
        MainMenu.SetActive(false);
        CreditsMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url);
    }
}
