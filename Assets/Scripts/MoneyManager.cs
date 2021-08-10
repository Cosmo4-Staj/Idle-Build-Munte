using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    StoneworkerController stoneworkerController;
    public TextMeshProUGUI totalMoneyText;
    public TextMeshProUGUI stoneworkerMoneyText;
    public TextMeshProUGUI minerMoneyText;
    public TextMeshProUGUI strengthMoneyText;

    public int currentMoney = 0;

    public Button stoneworkerButton;
    public Button minerButton;
    public Button powerButton;

    public bool isFull = false;
    public int fee;
    public int stoneworkerSub = 5;
    public int minerSub = 7;
    public int strengthSub = 15;

    void Start()
    {
        stoneworkerController = FindObjectOfType<StoneworkerController>();

        //currentMoney = 15;
        totalMoneyText = GetComponent<TextMeshProUGUI>();
        stoneworkerButton.interactable = false;
        minerButton.interactable = false;
        powerButton.interactable = false;
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

        if (currentMoney >= 15)
        {
            Debug.Log("larger than 15");
            stoneworkerButton.interactable = true;
            minerButton.interactable = true;
            powerButton.interactable = true;
        }
        else if (currentMoney >= 7)
        {
            Debug.Log("larger than 7");
            stoneworkerButton.interactable = true;
            minerButton.interactable = true;
        }

        else if (currentMoney >= 5 && currentMoney < 7)
        {
            Debug.Log("between 5-7");
            stoneworkerButton.interactable = true;
        }
    }

    public void StoneworkerFee()
    {
        int.TryParse(this.stoneworkerMoneyText.text, out stoneworkerSub);
        SubtractMoney(stoneworkerSub);
        stoneworkerSub += 3;
        stoneworkerMoneyText.text = stoneworkerSub.ToString();
    }

    public void MinerFee()
    {
        int.TryParse(this.minerMoneyText.text, out minerSub);
        SubtractMoney(minerSub);
        minerSub += 3;
        minerMoneyText.text = minerSub.ToString();
    }

    public void StrengthFee()
    {
        int.TryParse(this.strengthMoneyText.text, out strengthSub);
        SubtractMoney(strengthSub);
        strengthSub += 3;
        strengthMoneyText.text = strengthSub.ToString();
    }

    public void SubtractMoney(int subtractAmount) 
    {
        if(currentMoney - subtractAmount < 0)
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
                powerButton.interactable = false;
            }
        }
    }
}
