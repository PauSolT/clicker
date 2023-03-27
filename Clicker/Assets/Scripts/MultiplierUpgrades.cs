using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MultiplierUpgrades : MonoBehaviour
{
    public GeneratorUI generator;
    public Upgrades upgradeInfo;
    Clicker clicker;
    Button button;
    private void Start()
    {
        clicker = FindObjectOfType<Clicker>();
        button = GetComponent<Button>();
        button.onClick.AddListener(UnlockUpgrade);

        GetComponentsInChildren<TextMeshProUGUI>()[0].text = upgradeInfo.upgradeName;
        GetComponentsInChildren<TextMeshProUGUI>()[1].text = upgradeInfo.upgradeDescription + " " +upgradeInfo.increasedMultilpier.ToString();
        GetComponentsInChildren<TextMeshProUGUI>()[2].text = ClickerUI.TextGoldHelper(upgradeInfo.cost);

        if (upgradeInfo.unlocked)
            Unlocked();
    }

    public void UnlockUpgrade()
    {
        if (!upgradeInfo.unlocked && 
            clicker.Money >= upgradeInfo.cost)
        {
            clicker.Money -= upgradeInfo.cost;
            //generator.generator.multiplier *= upgradeInfo.increasedMultilpier;
            //upgradeInfo.unlocked = true;
            generator.generator.CalculateCurrentBaseGoldGenerator();
            generator.generator.CalculateCurrentGoldGenerator();
            generator.UpdateUpgradeTexts();
            clicker.clickerUI.UpdateMoneyText(clicker.Money);
            clicker.CalculateMoneyPerSecond();
            Unlocked();
        }
    }

    void Unlocked()
    {
        GetComponentsInChildren<TextMeshProUGUI>()[2].text = "Unlocked";
        button.interactable = false;
        transform.SetAsLastSibling();
    }

}
