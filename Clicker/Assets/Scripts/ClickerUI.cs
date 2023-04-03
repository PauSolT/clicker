using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ClickerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI moneyPerSecondText;
    public List<TextMeshProUGUI> clickMoneyList;
    public List<ParticleSystem> particles;
    public Queue<TextMeshProUGUI> clickMoneyText;
    public List<UpgradeClickEarning> upgradeClickEarnings;

    int i = 0;
    public int ClicksUnlocked { get; set; } = 0;

    public void Init()
    {
        clickMoneyText = new();
        foreach (TextMeshProUGUI txt in clickMoneyList)
        {
            clickMoneyText.Enqueue(txt);
            txt.gameObject.SetActive(false);
        }

        foreach (UpgradeClickEarning uc in upgradeClickEarnings)
        {
            if (uc.upgradedClick.unlocked)
            {
                ClicksUnlocked++;
            }
        }
        SetParticlesBurstCount(ClicksUnlocked);
        
    }

    public void SetParticlesBurstCount(int unlocked)
    {
        switch (unlocked)
        {
            case 1:
                foreach (ParticleSystem ps in particles)
                {
                    ParticleSystem.Burst em = ps.emission.GetBurst(0); ;
                    em.minCount = 3;
                    em.maxCount = 5;
                    ps.emission.SetBurst(0, em);
                }
                break;
            case 2:
                foreach (ParticleSystem ps in particles)
                {
                    ParticleSystem.Burst em = ps.emission.GetBurst(0); ;
                    em.minCount = 5;
                    em.maxCount = 7;
                    ps.emission.SetBurst(0, em);
                }
                break;
            case 3:
                foreach (ParticleSystem ps in particles)
                {
                    ParticleSystem.Burst em = ps.emission.GetBurst(0); ;
                    em.minCount = 7;
                    em.maxCount = 10;
                    ps.emission.SetBurst(0, em);
                }
                break;
            case 4:
                foreach (ParticleSystem ps in particles)
                {
                    ParticleSystem.Burst em = ps.emission.GetBurst(0); ;
                    em.minCount = 10;
                    em.maxCount = 15;
                    ps.emission.SetBurst(0, em);
                }
                break;
            case 0:
            default:
                foreach (ParticleSystem ps in particles)
                {
                    ParticleSystem.Burst em = ps.emission.GetBurst(0); ;
                    em.minCount = 1;
                    em.maxCount = 3;
                    ps.emission.SetBurst(0, em);
                }
                break;
        }
    }

    public void UpdateMoneyText(double money)
    {
        moneyText.text = TextGoldHelper(money);
    }

    public void UpdatMoneyPerSecondText(double moneyPerSecond)
    {
        moneyPerSecondText.text = TextGoldHelper(moneyPerSecond)+ "/s" ;
    }

    public void PlayParticles(Vector3 pos)
    {
        particles[i].transform.position = pos;
        particles[i].Play();

        i++;
        if (i == particles.Count)
            i = 0;
    }


    public static string TextGoldHelper(double money)
    {
        double numStr;
        string suffix;
        if (money < 1000d)
        {
            numStr = money;
            suffix = "";
        }
        else if (money < 1000000d)
        {
            numStr = money / 1000d;
            suffix = "K";
        }
        else if (money < 1000000000d)
        {
            numStr = money / 1000000d;
            suffix = "M";
        }
        else if (money < 1000000000000d)
        {
            numStr = money / 1000000000d;
            suffix = "B";
        }
        else if (money < 1000000000000000d)
        {
            numStr = money / 1000000000000d;
            suffix = "T";
        }
        else 
        {
            numStr = money / 1000000000000000d;
            suffix = "Q";
        }

        return numStr.ToString("0.##") + suffix + " G";
    }
}
