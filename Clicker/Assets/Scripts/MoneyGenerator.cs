using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "MoneyGenerator  ", order = 1)]
public class MoneyGenerator : ScriptableObject
{
    Clicker clicker;

    public double baseGoldGenerator;
    public double currentBaseGoldGenerator;
    public double currentGoldGenerator;
    public double baseCost;
    public double currentCost;
    public float multiplier = 1f;

    public int numberOfGenerators = 0;


    public void Init()
    {
        CalculateCurrentCost();
        CalculateCurrentBaseGoldGenerator();
        CalculateCurrentGoldGenerator();
        clicker = FindObjectOfType<Clicker>();

    }
    public double GetGold()
    {
        CalculateCurrentGoldGenerator();
        return currentGoldGenerator;
    }

    public void UnlockGenerator()
    {
        if (clicker.Money >= currentCost)
        {
            clicker.Money -= currentCost;
            clicker.clickerUI.UpdateMoneyText(clicker.Money);
            numberOfGenerators++;
            CalculateCurrentCost();
            CalculateCurrentBaseGoldGenerator();
            CalculateCurrentGoldGenerator();
            clicker.CalculateMoneyPerSecond();
        }
    }

    void CalculateCurrentCost()
    {
        currentCost = baseCost * (double)Mathf.Pow(1.15f, (float)numberOfGenerators);
    }

    public void CalculateCurrentBaseGoldGenerator()
    {
        currentBaseGoldGenerator = baseGoldGenerator * (double)multiplier;
    }

    public void CalculateCurrentGoldGenerator()
    {
        currentGoldGenerator = currentBaseGoldGenerator * numberOfGenerators;
    }


}
