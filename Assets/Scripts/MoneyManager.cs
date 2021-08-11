using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    GameUI gameUI;

    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI stoneworkerMoneyText;
    public TextMeshProUGUI minerMoneyText;
    public TextMeshProUGUI strengthMoneyText;

    public int currentMoney = 0;

    public Button stoneworkerButton;
    public Button minerButton;
    public Button strengthButton;
    public Button superworkerButton;

    public bool isFull = false;
    public int stoneworkerSub;
    public int minerSub;
    public int strengthSub;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        totalMoneyText = GetComponent<TextMeshProUGUI>();
        stoneworkerButton.interactable = false;
        minerButton.interactable = false;
        strengthButton.interactable = false;

        stoneworkerSub = 5;
        minerSub = 7;
        strengthSub = 15;
    }

    void Update()
    {
        totalMoneyText.text = currentMoney.ToString();

        if (isFull)
        {
            minerButton.interactable = false;
        }
    }

    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        CheckMoney();
        SuperworkerCheck();
    }

    //---------------------------------------------------------------------------------------------------------------------

    //Since there is an increase in price every time the button is pressed, to see if the money is enough each time
    public void StoneworkerFee()
    {
        int.TryParse(this.stoneworkerMoneyText.text, out stoneworkerSub);
        SubtractMoney(stoneworkerSub);
        stoneworkerSub += 3;
        stoneworkerMoneyText.text = stoneworkerSub.ToString();
        CheckMoney();
    }

    public void MinerFee()
    {
        int.TryParse(this.minerMoneyText.text, out minerSub);
        SubtractMoney(minerSub);
        minerSub += 3;
        minerMoneyText.text = minerSub.ToString();
        CheckMoney();
    }

    public void StrengthFee()
    {
        int.TryParse(this.strengthMoneyText.text, out strengthSub);
        SubtractMoney(strengthSub);
        strengthSub += 3;
        strengthMoneyText.text = strengthSub.ToString();
        CheckMoney();
    }

    //---------------------------------------------------------------------------------------------------------------------

    //payment for workers in each button tap
    public void SubtractMoney(int subtractAmount)
    {
        if (currentMoney - subtractAmount < 0)
        {
            Debug.Log("Not enough money");
        }
        else
        {
            currentMoney -= subtractAmount;
            if (currentMoney - subtractAmount < 0)
            {
                stoneworkerButton.interactable = false;
                minerButton.interactable = false;
                strengthButton.interactable = false;
            }
        }
    }

    public void SuperworkerCheck() {
        if (currentMoney >= 100 && gameUI.progress >= 0.5 && GameManager.instance.superworkerButton.gameObject.activeSelf == true)
        {
            superworkerButton.interactable = true;
        }
        else
        {
            superworkerButton.interactable = false;
        }
    }

    void CheckMoney()
    {
        if (currentMoney >= stoneworkerSub)
        {
            stoneworkerButton.interactable = true;
        }
        else if (currentMoney < stoneworkerSub)
        {
            stoneworkerButton.interactable = false;
        }
        if (currentMoney >= minerSub)
        { 
            minerButton.interactable = true;
        }
        else if (currentMoney < minerSub)
        {
            minerButton.interactable = false;
        }
        if (currentMoney >= strengthSub)
        {
            strengthButton.interactable = true;
        }
        else if (currentMoney < strengthSub)
        {
            strengthButton.interactable = false;
        }

        
    }
}
