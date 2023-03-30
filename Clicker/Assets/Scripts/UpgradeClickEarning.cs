using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeClickEarning : MonoBehaviour
{
    public UpgradedClick upgradedClick;

    Clicker clicker;
    TextMeshProUGUI text;
    Button button;
    private void Start()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>()[^1];
        clicker = FindObjectOfType<Clicker>();
        button = GetComponent<Button>();

        if (upgradedClick.unlocked)
        {
            Unlocked();
        }
        else
        {
            text.text = ClickerUI.TextGoldHelper(upgradedClick.cost);
            button.onClick.AddListener(UnlockUpgrade);
        }
    }

    public void UnlockUpgrade()
    {
        if (!upgradedClick.unlocked && clicker.Money >= upgradedClick.cost)
        {
            clicker.Money -= upgradedClick.cost;
            clicker.clickerUI.UpdateMoneyText(clicker.Money);
            SoundManager.Instance.PlayUpgrade();
            clicker.PerCerntEarning += 0.25d;
            upgradedClick.unlocked = true;
            Unlocked();
            clicker.clickerUI.ClicksUnlocked++;
            clicker.clickerUI.SetParticlesBurstCount(clicker.clickerUI.ClicksUnlocked);
        }
    }

    void Unlocked()
    {
        text.text = "Unlocked";
        button.interactable = false;
        transform.SetAsLastSibling();
    }
   
}
