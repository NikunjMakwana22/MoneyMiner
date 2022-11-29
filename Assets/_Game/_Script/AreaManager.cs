using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AreaManager : MonoBehaviour
{

    public Temp_GoldSorter Temp_GoldSorter;
    public Animator MachineAnim;
    public GameObject GoldBarPrefab;

    public Player player;
    private int Temp;
   [SerializeField] private int AmountInMachine;


    [SerializeField] private bool UpgradeArea;
    [SerializeField] private bool IsPlayerInside=false,IsUpgradeCanvasVisible=false;



    [SerializeField] private float CurrentTime, DelayTime;
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

               
                if (player.AmountInBag > 0)
                {
                    AmountInMachine = (int)player.AmountInBag/100;
                   // player.CurrentAmount += player.AmountInBag;
                    player.AmountInBag = 0;
                    player.IsBagFull = false;
                    player.FullText.enabled = false;
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

        if(AmountInMachine>0)
        {
            MachineAnim.SetBool("Play", true);
            CurrentTime -= 1 * Time.deltaTime;
            if (CurrentTime < 0.1f)
            {
                AmountInMachine--;
                Temp_GoldSorter.InstGold();
                player.CurrentAmount++;
                ButtonManager.Instance.UpdateCashText();
                if (AmountInMachine<1)
                    MachineAnim.SetBool("Play", false);
                CurrentTime = DelayTime;
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
