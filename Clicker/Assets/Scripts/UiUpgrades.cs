using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpgrades : MonoBehaviour
{
    public GameObject generators;
    public GameObject coinsUpgrades;
    public GameObject diamondsUpgrades;
    public GameObject otherUpgrades;

    
    public void ShowGenerators()
    {
        coinsUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(false);
        generators.SetActive(true);
    }

    public void ShowCoinsUpgrade()
    {
        generators.SetActive(false);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(false);
        coinsUpgrades.SetActive(true);

        foreach (Transform go in coinsUpgrades.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform)
        {
            go.gameObject.SetActive(true);
            go.TryGetComponent(out MultiplierUpgrades upgrade);

            if (upgrade && !upgrade.upgradeInfo.genUnlocked)
                go.gameObject.SetActive(false);
        }
    }

    public void ShowDiamondsUpgrade()
    {
        generators.SetActive(false);
        coinsUpgrades.SetActive(false);
        otherUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(true);
        foreach (Transform go in diamondsUpgrades.transform.GetChild(0).transform.GetChild(0).transform.GetChild(0).transform)
        {
            go.gameObject.SetActive(true);
            go.TryGetComponent(out MultiplierUpgrades upgrade);

            if (upgrade && !upgrade.upgradeInfo.genUnlocked)
                go.gameObject.SetActive(false);
        }
    }

    public void ShowOtherUpgrade()
    {
        generators.SetActive(false);
        coinsUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(true);
    }

}
