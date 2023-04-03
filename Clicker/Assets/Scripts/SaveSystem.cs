using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveSystem : MonoBehaviour
{
    public GeneratorUI[] generators;
    public MultiplierUpgrades[] upgrades;
    public UpgradeClickEarning[] clickUpgrades;
    Clicker clicker;

    
    void Awake()
    {
        clicker = FindObjectOfType<Clicker>();

        LoadSave();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            StartSave();
        }

        if (!pause)
        {
            LoadSave();
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            StartSave();
        }

        if (focus)
        {
            LoadSave();
        }
    }

    public void StartSave()
    {
        PlayerPrefs.SetString("money", clicker.Money.ToString());
        PlayerPrefs.SetInt("muted", SoundManager.Instance.muted);
        PlayerPrefs.SetString("sysString", DateTime.Now.ToBinary().ToString());
        SaveGenerators();
        SaveUpgrades();
        SaveClickUpgrades();
    }

    public void LoadSave()
    {
        bool canParse = double.TryParse(PlayerPrefs.GetString("money"), out double num);
        if (canParse)
            clicker.Money = num;
        else
            clicker.Money = 0;

        SoundManager.Instance.muted = PlayerPrefs.GetInt("muted", 0);
        if (PlayerPrefs.HasKey("sysString"))
        {
            clicker.CalculateMoneyPerSecond();
            DateTime currentDate = DateTime.Now;
            long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
            DateTime oldDate = DateTime.FromBinary(temp);
            TimeSpan difference = currentDate.Subtract(oldDate);
            clicker.Money += System.Math.Round(difference.TotalSeconds * clicker.EarningPerSecond);
        }

        LoadGenerators();
        LoadUpgrades();
        LoadUpgrades();
    }

    void SaveGenerators()
    {
        MoneyGenerator moneyGenerator = generators[0].generator;
        const string cbgg = nameof(moneyGenerator.currentBaseGoldGenerator);
        const string cgg = nameof(moneyGenerator.currentGoldGenerator);
        const string cc = nameof(moneyGenerator.currentCost);
        const string m = nameof(moneyGenerator.multiplier);
        const string nog = nameof(moneyGenerator.numberOfGenerators);

        foreach (GeneratorUI generator in generators)
        {
            MoneyGenerator gen = generator.generator;

            PlayerPrefs.SetString(cbgg + generator.name, gen.currentBaseGoldGenerator.ToString());
            PlayerPrefs.SetString(cgg + generator.name, gen.currentGoldGenerator.ToString());
            PlayerPrefs.SetString(cc + generator.name, gen.currentCost.ToString());
            PlayerPrefs.SetString(m + generator.name, gen.multiplier.ToString());
            PlayerPrefs.SetString(nog + generator.name, gen.numberOfGenerators.ToString());
        }
    }


    void LoadGenerators()
    {
        MoneyGenerator moneyGenerator = generators[0].generator;
        const string cbgg = nameof(moneyGenerator.currentBaseGoldGenerator);
        const string cgg = nameof(moneyGenerator.currentGoldGenerator);
        const string cc = nameof(moneyGenerator.currentCost);
        const string m = nameof(moneyGenerator.multiplier);
        const string nog = nameof(moneyGenerator.numberOfGenerators);

        foreach (GeneratorUI generator in generators)
        {
            MoneyGenerator gen = generator.generator;

            gen.currentBaseGoldGenerator = double.Parse(PlayerPrefs.GetString(cbgg + generator.name, gen.currentBaseGoldGenerator.ToString()));
            gen.currentGoldGenerator = double.Parse(PlayerPrefs.GetString(cgg + generator.name, gen.currentGoldGenerator.ToString()));
            gen.currentCost = double.Parse(PlayerPrefs.GetString(cc + generator.name, gen.currentCost.ToString()));
            gen.multiplier = float.Parse(PlayerPrefs.GetString(m + generator.name, gen.multiplier.ToString()));
            gen.numberOfGenerators = int.Parse(PlayerPrefs.GetString(nog + generator.name, gen.numberOfGenerators.ToString()));
        }
    }

    void SaveUpgrades()
    {
        Upgrades upgrade = upgrades[0].upgradeInfo;
        const string un = nameof(upgrade.unlocked);
        const string genun = nameof(upgrade.genUnlocked);

        foreach (MultiplierUpgrades multiplierUpgrades in upgrades)
        {
            Upgrades upg = multiplierUpgrades.upgradeInfo;

            PlayerPrefs.SetString(un + multiplierUpgrades.name, upg.unlocked.ToString());
            PlayerPrefs.SetString(genun + multiplierUpgrades.name, upg.genUnlocked.ToString());
        }
    }

    void LoadUpgrades()
    {
        Upgrades upgrade = upgrades[0].upgradeInfo;
        const string un = nameof(upgrade.unlocked);
        const string genun = nameof(upgrade.genUnlocked);

        foreach (MultiplierUpgrades multiplierUpgrades in upgrades)
        {
            Upgrades upg = multiplierUpgrades.upgradeInfo;

            upg.unlocked = bool.Parse(PlayerPrefs.GetString(un + multiplierUpgrades.name, upg.unlocked.ToString()));
            upg.genUnlocked = bool.Parse(PlayerPrefs.GetString(genun + multiplierUpgrades.name, upg.genUnlocked.ToString()));
        }
    }

    void SaveClickUpgrades()
    {
        UpgradedClick upgrade = clickUpgrades[0].upgradedClick;
        const string un = nameof(upgrade.unlocked);

        foreach (UpgradeClickEarning multiplierUpgrades in clickUpgrades)
        {
            UpgradedClick upg = multiplierUpgrades.upgradedClick;

            PlayerPrefs.SetString(un + multiplierUpgrades.name, upg.unlocked.ToString());
        }
    }

    void LoadClickUpgrades()
    {
        UpgradedClick upgrade = clickUpgrades[0].upgradedClick;
        const string un = nameof(upgrade.unlocked);

        foreach (UpgradeClickEarning multiplierUpgrades in clickUpgrades)
        {
            UpgradedClick upg = multiplierUpgrades.upgradedClick;

            upg.unlocked = bool.Parse(PlayerPrefs.GetString(un + multiplierUpgrades.name, upg.unlocked.ToString()));
        }
    }
}
