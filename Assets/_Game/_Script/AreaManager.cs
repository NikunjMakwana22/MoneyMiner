using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaManager : MonoBehaviour
{

    public Player player;
    public TextMeshProUGUI CashText;
    private int Temp;



    [SerializeField] private bool UpgradeArea;
    [SerializeField] private bool IsPlayerInside=false,IsUpgradeCanvasVisible=false;



   // [SerializeField] private float CurrentTime, DelayTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerInside)
        {
            if (!UpgradeArea)
            {
                //DepositeMoney

                //CurrentTime -= 1 * Time.deltaTime;
                //     if(CurrentTime<0.1f && player.AmountInBag>0)
                //     {
                //        player.AmountInBag--;
                //        player.CurrentAmount++;

                //        CurrentTime = DelayTime;
                //     }
                if (player.AmountInBag > 0)
                {
                    player.CurrentAmount += player.AmountInBag;
                    player.AmountInBag = 0;
                    player.IsBagFull = false;
                    player.CapacityProgressBar.fillAmount = 0f;
                    ButtonManager.Instance.UpdateCashText();
                }
            }
            else
            {
                if(!IsUpgradeCanvasVisible)
                {
                    IsUpgradeCanvasVisible = true;
                    ButtonManager.Instance.EnableUpgradeMenu();
                }
            }
        }
       
        
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !UpgradeArea)
        {
            Debug.Log("DepositeArea");
            IsPlayerInside = true;
        }
        else
        {
            Debug.Log("UpgradeArea");
            IsPlayerInside = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        IsPlayerInside = false;
        IsUpgradeCanvasVisible = false;
        ButtonManager.Instance.DisableUpgradeMenu();
    }
}
