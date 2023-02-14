using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    // Start is called before the first frame update
    public void SetMaxWater(int water)
    {
        slider.maxValue = water;
        slider.value = water;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetWater(int water)
    {
        slider.value = water;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
