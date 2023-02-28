using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public float Money { get; set; } = 0;

    float baseEarning = 1;

    public void ClickMoney()
    {
        Money += baseEarning;
    }
}
