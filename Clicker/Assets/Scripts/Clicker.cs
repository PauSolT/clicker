using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public float Money { get; set; } = 0;

    float baseEarning = 1;
    float earningPerSecond = 1;

    private void Start()
    {
        InvokeRepeating(nameof(MoneyPerSecond), 1f, 1f);
    }

    public void ClickMoney()
    {
        Money += baseEarning;
        earningPerSecond++;
    }

    void MoneyPerSecond()
    {
        Money += earningPerSecond;
    }

}
