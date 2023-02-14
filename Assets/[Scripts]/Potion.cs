using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Potion : MonoBehaviour, ICollectible
{
    public static event Action OnPotionCollected; 
    public void Collect()
    {
        Destroy(gameObject);
        OnPotionCollected?.Invoke();
    }


}
