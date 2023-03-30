using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorUI : MonoBehaviour
{
    public MoneyGenerator generator;
    TextMeshProUGUI cost;
    TextMeshProUGUI number;
    TextMeshProUGUI totalGoldSecond;
    TextMeshProUGUI baseGoldSecond;

    public MultiplierUpgrades[] upgrades;

    public void Start()
    {
        generator.Init();
        cost = GetComponentsInChildren<TextMeshProUGUI>()[0];
        number = GetComponentsInChildren<TextMeshProUGUI>()[1];
        totalGoldSecond = GetComponentsInChildren<TextMeshProUGUI>()[3];
        baseGoldSecond = GetComponentsInChildren<TextMeshProUGUI>()[4];

        UpdateCost();
        UpdateNumber();
        UpdateTotalGoldSecond();
        UpdateBaseGoldSecond();

        Button button = GetComponent<Button>();
        button.onClick.AddListener(generator.UnlockGenerator);
        button.onClick.AddListener(UpdateCost);
        button.onClick.AddListener(UpdateNumber);
        button.onClick.AddListener(UpdateTotalGoldSecond);
        button.onClick.AddListener(UnlockUpgrades);
    }

    public void UnlockUpgrades()
    {
        foreach (MultiplierUpgrades go in upgrades)
        {
            go.upgradeInfo.genUnlocked = true;
        }
    }

    public void UpdateCost()
    {
        cost.text = ClickerUI.TextGoldHelper(generator.currentCost);
    }

    public void UpdateNumber()
    {
        number.text = generator.numberOfGenerators.ToString();
    }

    public void UpdateTotalGoldSecond()
    {
        totalGoldSecond.text = ClickerUI.TextGoldHelper(generator.GetGold()) + "/s";
    }

    public void UpdateBaseGoldSecond()
    {
        baseGoldSecond.text = ClickerUI.TextGoldHelper(generator.currentBaseGoldGenerator) + "/s";
    }

    public void UpdateUpgradeTexts()
    {
        UpdateTotalGoldSecond();
        UpdateBaseGoldSecond();
    }

}
