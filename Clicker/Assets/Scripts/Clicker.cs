using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public double Money { get; set; } = 10000;

    float baseEarning = 1;
    public double EarningPerSecond { get; set; } = 0;

    public ClickerUI clickerUI;
    public MoneyGenerator[] generators;

    float timer = 0;

    private void Start()
    {
        CalculateMoneyPerSecond();
        clickerUI.UpdatMoneyPerSecondText(EarningPerSecond);
        clickerUI.UpdateMoneyText(Money);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f / EarningPerSecond)
        {
            MoneyPerSecond();
            timer = 0;
        }
    }

    public void ClickMoney()
    {
        Money += baseEarning;
        clickerUI.UpdateMoneyText(Money);
    }

    void MoneyPerSecond()
    {
        CalculateMoneyPerSecond();
        Money += 1;
        clickerUI.UpdateMoneyText(Money);
    }

    public void CalculateMoneyPerSecond()
    {
        double finalEarnings = 0;
        foreach (MoneyGenerator generator in generators)
        {
            finalEarnings += generator.GetGold();
        }
        EarningPerSecond = finalEarnings;

        clickerUI.UpdatMoneyPerSecondText(EarningPerSecond);
    }



}
