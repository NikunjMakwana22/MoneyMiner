using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ButtonManager : MonoBehaviour
{

    public static ButtonManager Instance;
    public Player Player;
    public Button PowerButton, RangeButton, CapacityButton;
    public GameObject UpgradeCanvas;
    public TextMeshProUGUI CashText;

    private void Awake()
    {
        Instance = this;
    }


    public void CheckCurrentAmount()
    {
        UpdateCashText();
        if (GameManager.Instance.NextCapacityAmount <= Player.CurrentAmount)
            CapacityButton.interactable = true;
        else
            CapacityButton.interactable = false;

        if (GameManager.Instance.NextPowerAmount <= Player.CurrentAmount)
            PowerButton.interactable = true;
        else
            PowerButton.interactable = false;

        if (GameManager.Instance.NextRangeAmount <= Player.CurrentAmount)
            RangeButton.interactable = true;
        else
            RangeButton.interactable = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }


    public void UpgradePower()
    {
        Player._speed += 1;
        Player.CurrentAmount -= GameManager.Instance.NextPowerAmount;
        Player.GetCurrentValue();
        CheckCurrentAmount();
    }

    public void UpgradeRange()
    {
        GameManager.Instance.CurrentRangeLevel += 1;
        Player.CurrentAmount -= GameManager.Instance.NextRangeAmount;
        Player.GetCurrentValue();
        CheckCurrentAmount();
    }

    public void UpgradeCapacity()
    {
        GameManager.Instance.CurrentCapacityLevel += 1;
        Player.CurrentAmount -= GameManager.Instance.NextCapacityAmount;
        Player.GetCurrentValue();
        CheckCurrentAmount();
    }





    public void EnableUpgradeMenu()
    {
       
        UpgradeCanvas.SetActive(true);
        CheckCurrentAmount();
    }
    public void DisableUpgradeMenu()
    {
        UpgradeCanvas.SetActive(false);
    }


    public void UpdateCashText()
    {
        CashText.text = Player.CurrentAmount.ToString();
    }
}
