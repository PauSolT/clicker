using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "UpgradeGenerator", order = 2)]
public class Upgrades : ScriptableObject
{
    public float increasedMultilpier;
    public double cost;
    public bool unlocked = false;
    public bool genUnlocked = false;

    public string upgradeName;
    public string upgradeDescription;

}
