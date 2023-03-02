using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    public void UpdateMoneyText(float money)
    {
        moneyText.text = money.ToString();
    }

   

}
