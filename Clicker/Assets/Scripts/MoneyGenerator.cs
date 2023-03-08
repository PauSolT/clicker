using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "MoneyGenerator  ", order = 1)]
public class MoneyGenerator : ScriptableObject
{
    Clicker clicker;

    public float baseGoldGenerator;
    public float currentBaseGoldGenerator;
    public float currentGoldGenerator;
    public float baseCost;
    public float currentCost;
    float multiplier = 1;

    public int numberOfGenerators = 0;


    public void Init()
    {
        CalculateCurrentCost();
        CalculateCurrentBaseGoldGenerator();
        CalculateCurrentGoldGenerator();
        clicker = FindObjectOfType<Clicker>();
    }
    public float GetGold()
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
        }
    }

    void CalculateCurrentCost()
    {
        currentCost = baseCost * Mathf.Pow(1.15f, numberOfGenerators);
    }

    void CalculateCurrentBaseGoldGenerator()
    {
        currentBaseGoldGenerator = baseGoldGenerator * multiplier;
    }

    void CalculateCurrentGoldGenerator()
    {
        currentGoldGenerator = currentBaseGoldGenerator * numberOfGenerators;
    }


}
