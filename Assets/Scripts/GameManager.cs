using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI HumansText;

    private List<Human> humans;
    private bool killedOne;
    private int startingCount;

    void Start()
    {
        humans = Object.FindObjectsOfType<Human>().ToList();
        startingCount = humans.Count;
    }

    void Update()
    {
        humans = humans.Where(h => h != null && !h.IsDead).ToList();

        if (humans.Count < startingCount)
        {
            killedOne = true;
        }

        if (!killedOne)
        {
            HumansText.text = $"Go forth";
        }
        else
        {
            if (humans.Count > 10)
            {
                HumansText.text = $"Slay them all!";
            }
            else if (humans.Count > 1)
            {
                HumansText.text = $"{humans.Count} knights remain!";
            }
            else if (humans.Count == 1)
            {
                HumansText.text = $"Leave no survivors!";
            }
            else
            {
                HumansText.text = "You have done well";
            }
        }
    }
}
