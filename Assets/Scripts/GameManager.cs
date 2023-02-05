using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI HumansText;
    public GameObject PauseMenu;

    private List<Human> humans;
    private int killCount;
    private int startingCount;
    private bool paused;

    void Start()
    {
        humans = Object.FindObjectsOfType<Human>().ToList();
        startingCount = humans.Count;
        Resume();
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Time.timeScale < float.Epsilon)
        {
            return;
        }

        humans = humans.Where(h => h != null && !h.IsDead).ToList();

        killCount = startingCount - humans.Count;

        if (killCount == 0)
        {
            HumansText.text = "Go forth";
        }
        else if (killCount == 1)
        {
            HumansText.text = "Slay them all!";
        }
        else if (humans.Count > 1 && humans.Count <= 5)
        {
            HumansText.text = $"{humans.Count} knights remain!";
        }
        else if (humans.Count == 1)
        {
            HumansText.text = "Leave no survivors!";
        }
        else if (humans.Count == 0)
        {
            HumansText.text = "You have done well";
        }
        else
        {
            HumansText.text = "";
        }
    }

    public void Pause()
    {
        Cursor.visible = true;
        paused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Cursor.visible = false;
        paused = false;
        PauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void QuitToMenu()
    {
        Cursor.visible = true;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
