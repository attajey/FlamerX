using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FuelText : MonoBehaviour
{
    static int fuelCount = 0;
    private Text fuelText;
    // Start is called before the first frame update
    void Start()
    {
        fuelText = GetComponent<Text>();
        fuelText.text = $"Fuel: {fuelCount}";

    }

    private void OnEnable()
    {
        Fuel.OnFuelCollected += IncreamentFuelCount;
    }

    private void OnDisable()
    {

        Fuel.OnFuelCollected -= IncreamentFuelCount;
    }

    public void IncreamentFuelCount()
    {
        fuelCount++;
        fuelText.text = $"Fuel: {fuelCount}";
        //cointText.SetText($"Coins: {coinCount}");
    }
}
