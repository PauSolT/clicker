using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "UpgradeClick", order = 3)]
public class UpgradedClick : ScriptableObject
{
    public bool unlocked = false;
    public double cost;
}
