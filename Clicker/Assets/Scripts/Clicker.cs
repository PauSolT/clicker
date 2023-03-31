using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Clicker : MonoBehaviour
{
    public double Money { get; set; } = 0d;

    readonly double baseEarning = 1d;
    public double UpgradedEarning { get; set; } = 0d;
    public double PerCerntEarning { get; set; } = 0d;
    public double EarningPerSecond { get; set; } = 0d;

    public ClickerUI clickerUI;
    public MoneyGenerator[] generators;
    public List<UpgradeClickEarning> upgradeClickEarnings;

    float timer = 0;

    DateTime currentDate;
    DateTime oldDate;

    private void Start()
    {
        StartSave();
        clickerUI.Init();
        CalculateMoneyPerSecond();
        PerCerntEarning = 0.25d * clickerUI.ClicksUnlocked;
        
        currentDate = DateTime.Now;
        long temp = Convert.ToInt64(PlayerPrefs.GetString("sysString"));
        oldDate = DateTime.FromBinary(temp);
        TimeSpan difference = currentDate.Subtract(oldDate);
        Money += System.Math.Round(difference.TotalSeconds * EarningPerSecond);

        
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
        CalculateUpgradedEarning();
        Money += baseEarning + UpgradedEarning;
        clickerUI.UpdateMoneyText(Money);
        SoundManager.Instance.PlayClick();
        StartCoroutine(nameof(ClickTextDissapear));
    }

    void CalculateUpgradedEarning()
    {
        CalculateMoneyPerSecond();
        UpgradedEarning = EarningPerSecond * PerCerntEarning;
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

    private void OnApplicationQuit()
    {
        SaveData();
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            SaveData();

        if (focus)
            StartSave();
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("money", Money.ToString());
        PlayerPrefs.SetInt("muted", SoundManager.Instance.muted);
        PlayerPrefs.SetString("sysString", DateTime.Now.ToBinary().ToString());
    }

    private void StartSave()
    {
        Money = double.Parse(PlayerPrefs.GetString("money", "0d"));
    }


    IEnumerator ClickTextDissapear()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward);
        TMPro.TextMeshProUGUI text = clickerUI.clickMoneyText.Dequeue();
        text.gameObject.SetActive(true);
        text.transform.position = mousePos;

        text.text = "+" + ClickerUI.TextGoldHelper(baseEarning + UpgradedEarning);
        clickerUI.clickMoneyText.Enqueue(text);
        clickerUI.PlayParticles(mousePos);

        Color c = text.color;
        for (float alpha = 1f; alpha >= 0; alpha -= .01f)
        {
            c.a = alpha;
            text.color = c;
            text.transform.position += 1 * Vector3.up * Time.deltaTime;
            yield return new WaitForSeconds(.01f);
        }

        text.gameObject.SetActive(false);
        yield return null;
    }


}
