using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public static DebugMenu Instance;
    public Player player;
    public GameObject fPS;
    public int CurrentAmount;


    public bool Showfps, UM, UC;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void SetPlayerSpeed(Slider s)
    {

    }

    public void SetRotSpeed(Slider s)
    {

    }




    public void ToggleShowFPS()
    {
        if (Showfps)
        {
            Showfps = false;
            fPS.SetActive(false);
        }
        else
        {
            fPS.SetActive(true);
            Showfps = true;
        }
    }

    public void ToggleUM()
    {
        if (UM)
        {
            player.CurrentAmount = CurrentAmount;
            UM = false;
        }
        else
        {
            player.CurrentAmount = 1000000000;
            UM = true;
        }

    }
}
