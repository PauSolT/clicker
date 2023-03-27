using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyPerSecondText;
    public List<TextMeshProUGUI> clickMoneyList;
    public Queue<TextMeshProUGUI> clickMoneyText;

    private void Start()
    {
        clickMoneyText = new();
        foreach (TextMeshProUGUI txt in clickMoneyList)
        {
            clickMoneyText.Enqueue(txt);
        }
    }

    public void UpdateMoneyText(double money)
    {
        moneyText.text = TextGoldHelper(money);
    }

    public void UpdatMoneyPerSecondText(double moneyPerSecond)
    {
        moneyPerSecondText.text = TextGoldHelper(moneyPerSecond)+ "/s" ;
    }


    public static string TextGoldHelper(double money)
    {
        double numStr;
        string suffix;
        if (money < 1000d)
        {
            numStr = money;
            suffix = "";
        }
        else if (money < 1000000d)
        {
            numStr = money / 1000d;
            suffix = "K";
        }
        else if (money < 1000000000d)
        {
            numStr = money / 1000000d;
            suffix = "M";
        }
        else if (money < 1000000000000d)
        {
            numStr = money / 1000000000d;
            suffix = "B";
        }
        else if (money < 1000000000000000d)
        {
            numStr = money / 1000000000000d;
            suffix = "T";
        }
        else 
        {
            numStr = money / 1000000000000000d;
            suffix = "Q";
        }

        return numStr.ToString("0.##") + suffix + " G";
    }
}
