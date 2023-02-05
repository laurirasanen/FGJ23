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

    void Start()
    {
        humans = Object.FindObjectsOfType<Human>().ToList();    
    }

    void Update()
    {
        humans = humans.Where(h => h != null && !h.IsDead).ToList();

        if (humans.Count > 0)
        {
            HumansText.text = $"{humans.Count} knights remain!";
        }
        else
        {
            HumansText.text = "";
        }
    }
}
