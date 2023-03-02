using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneratorUI : MonoBehaviour
{
    public MoneyGenerator generator;
    TextMeshProUGUI cost;
    TextMeshProUGUI number;


    public void Start()
    {
        cost = GetComponentsInChildren<TextMeshProUGUI>()[0];
        number = GetComponentsInChildren<TextMeshProUGUI>()[1];

        Button button = GetComponent<Button>();
        button.onClick.AddListener(UpdateCost);
        button.onClick.AddListener(UpdateNumber);
    }

    public void UpdateCost()
    {
        cost.text = generator.currentCost.ToString();
    }

    public void UpdateNumber()
    {
        number.text = generator.numberOfGenerators.ToString();
    }

}
