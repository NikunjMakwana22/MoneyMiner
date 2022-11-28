using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{


    public Player Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForAmountInHand()
    {
       
    }





    public void UpgradePower()
    {   
        GameManager.Instance.CurrentPowerLevel += 1;
        Player.GetCurrentValue();
        CheckForAmountInHand();
    }

    public void UpgradeRange()
    {
        GameManager.Instance.CurrentRangeLevel += 1;
        Player.GetCurrentValue();
        CheckForAmountInHand();
    }

    public void UpgradeCapacity()
    {
        GameManager.Instance.CurrentCapacityLevel += 1;
        Player.GetCurrentValue();
        CheckForAmountInHand();
    }
}
