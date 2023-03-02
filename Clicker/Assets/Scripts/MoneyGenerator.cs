using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "MoneyGenerator  ", order = 1)]
public class MoneyGenerator : ScriptableObject
{
    Clicker clicker;

    public float baseGoldGenerator;
    public float baseCost;
    public float currentCost;
    float multiplier = 1;

    public int numberOfGenerators = 0;


    public void Init()
    {
        CalculateCurrentCost();
        clicker = FindObjectOfType<Clicker>();
    }
    public float GetGold()
    {
        return baseGoldGenerator * multiplier * numberOfGenerators;
    }

    public void UnlockGenerator()
    {
        if (clicker.Money >= currentCost)
        {
            clicker.Money -= currentCost;
            clicker.clickerUI.UpdateMoneyText(clicker.Money);
            numberOfGenerators++;
            CalculateCurrentCost();
        }
    }

    void CalculateCurrentCost()
    {
        currentCost = baseCost * Mathf.Pow(1.15f, numberOfGenerators);
    }


}
