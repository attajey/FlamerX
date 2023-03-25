using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Fuel : MonoBehaviour, ICollectible
{
    public static event Action OnFuelCollected;
    public void Collect()
    {
        Debug.Log("Fuel Collected");
        GlobalVariables.itemsCollected++;
        GlobalVariables.totalScore += 100;
        Destroy(gameObject);
        OnFuelCollected?.Invoke();
    }
}
