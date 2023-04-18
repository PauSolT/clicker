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

    float timer = 0;

    const float targetUpdateRate = 60f;

    public ClickerUI clickerUI;
    public MoneyGenerator[] generators;

    private void Start()
    {
        Application.targetFrameRate = -1;
        clickerUI.Init();
        CalculateMoneyPerSecond();
        PerCerntEarning = 0.25d * clickerUI.ClicksUnlocked;

        clickerUI.UpdatMoneyPerSecondText(EarningPerSecond);
        clickerUI.UpdateMoneyText(Money);

    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1f / targetUpdateRate)
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
        Money += EarningPerSecond / (targetUpdateRate / 2f);
        clickerUI.UpdateMoneyText(Money);
    }

    public void CalculateMoneyPerSecond()
    {
        double finalEarnings = 0d;
        foreach (MoneyGenerator generator in generators)
        {
            finalEarnings += generator.GetGold();
        }
        EarningPerSecond = finalEarnings;

        clickerUI.UpdatMoneyPerSecondText(EarningPerSecond);
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
            text.transform.position += 0.1f * Time.deltaTime * Vector3.up;
            yield return new WaitForSeconds(.01f);
        }

        text.gameObject.SetActive(false);
        yield return null;
    }


}
