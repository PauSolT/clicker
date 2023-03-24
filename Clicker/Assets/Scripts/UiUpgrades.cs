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
        generators.SetActive(true);
        coinsUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(false);
    }

    public void ShowCoinsUpgrade()
    {
        generators.SetActive(false);
        coinsUpgrades.SetActive(true);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(false);
    }

    public void ShowDiamondsUpgrade()
    {
        generators.SetActive(false);
        coinsUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(true);
        otherUpgrades.SetActive(false);
    }

    public void ShowOtherUpgrade()
    {
        generators.SetActive(false);
        coinsUpgrades.SetActive(false);
        diamondsUpgrades.SetActive(false);
        otherUpgrades.SetActive(true);
    }

}
