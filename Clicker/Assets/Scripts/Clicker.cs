using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public float Money { get; set; } = 10000000;

    float baseEarning = 1;
    float earningPerSecond = 1;

    public ClickerUI clickerUI;

    private void Start()
    {
        InvokeRepeating(nameof(MoneyPerSecond), 1f, 1f);
    }

    public void ClickMoney()
    {
        Money += baseEarning;
        clickerUI.UpdateMoneyText(Money);
    }

    void MoneyPerSecond()
    {
        Money += earningPerSecond;
        clickerUI.UpdateMoneyText(Money);
    }

}
