using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    Clicker clicker;
    private void Awake()
    {
        clicker = FindObjectOfType<Clicker>();
        UpdateMoneyText();
        InvokeRepeating(nameof(UpdateMoneyText), 1f, 1f);
    }

    public void UpdateMoneyText()
    {
        moneyText.text = clicker.Money.ToString();
    }
   
}
