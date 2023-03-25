using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Potion : MonoBehaviour, ICollectible
{
    public static event Action OnPotionCollected; 
    public void Collect()
    {
        GlobalVariables.itemsCollected++;
        GlobalVariables.totalScore += 250;
        Destroy(gameObject);
        OnPotionCollected?.Invoke();
    }


}
