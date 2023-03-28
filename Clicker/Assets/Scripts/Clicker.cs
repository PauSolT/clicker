using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public double Money { get; set; } = 10000000d;

    readonly double baseEarning = 1d;
    public double UpgradedEarning { get; set; } = 0d;
    public double PerCerntEarning { get; set; } = 0d;
    public double EarningPerSecond { get; set; } = 0d;

    public ClickerUI clickerUI;
    public MoneyGenerator[] generators;

    float timer = 0;

    private void Start()
    {
        //Money = double.Parse(PlayerPrefs.GetString("money", "0d"));
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
        CalculateUpgradedEarning();
        Money += baseEarning + UpgradedEarning;
        clickerUI.UpdateMoneyText(Money);
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
        PlayerPrefs.SetString("money", Money.ToString());
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
            PlayerPrefs.SetString("money", Money.ToString());

        //if (focus)
        //    Money = double.Parse(PlayerPrefs.GetString("money", "0d"));
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
